using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class SceneTransitionController : MonoBehaviour
{
    [SerializeField] private SceneLoadEventChannelSO requestSceneLoad;

    [Header("Fade Events")]
    [SerializeField] private FadeEventChannelSO fadeOut;
    [SerializeField] private FadeEventChannelSO fadeIn;
    [SerializeField] private float fadeDuration = 0.2f;

    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private PlayerInputGate inputGate;
    [SerializeField] private CinemachineCamera virtualCamera;

    private string _currentMapSceneName;

    private void OnEnable() => requestSceneLoad.OnRequested += HandleRequest;
    private void OnDisable() => requestSceneLoad.OnRequested -= HandleRequest;

    private void HandleRequest(SceneLoadRequest req)
    {
        StopAllCoroutines();
        StartCoroutine(TransitionRoutine(req));
    }

    private IEnumerator TransitionRoutine(SceneLoadRequest req)
    {
        inputGate.SetEnabled(false);

        // Fade Out (tela escurece)
        if (fadeOut) fadeOut.Raise(fadeDuration);
        yield return new WaitForSecondsRealtime(fadeDuration);

        // Load destino additive
        if (!SceneManager.GetSceneByName(req.sceneName).isLoaded)
        {
            var loadOp = SceneManager.LoadSceneAsync(req.sceneName, LoadSceneMode.Additive);
            while (!loadOp.isDone) yield return null;
        }

        // Ativa a nova cena
        var newScene = SceneManager.GetSceneByName(req.sceneName);
        SceneManager.SetActiveScene(newScene);

        // dá 1 frame para spawns inicializarem
        yield return null;

        // Move player para spawn
        var previousPlayerPosition = player.position;
        if (MovePlayerToSpawn(req.spawnId))
        {
            SnapCameraToPlayer(previousPlayerPosition, player.position);
        }

        // Descarrega mapa anterior
        if (!string.IsNullOrEmpty(_currentMapSceneName) && _currentMapSceneName != req.sceneName)
        {
            var unloadOp = SceneManager.UnloadSceneAsync(_currentMapSceneName);
            while (unloadOp != null && !unloadOp.isDone) yield return null;
        }

        _currentMapSceneName = req.sceneName;

        // Fade In (tela clareia)
        if (fadeIn) fadeIn.Raise(fadeDuration);
        yield return new WaitForSecondsRealtime(fadeDuration);

        inputGate.SetEnabled(true);
    }

    private bool MovePlayerToSpawn(string spawnId)
    {
        var roots = SceneManager.GetActiveScene().GetRootGameObjects();
        SpawnPointsRegistry registry = null;

        foreach (var go in roots)
        {
            Debug.Log(go);
            registry = go.GetComponentInChildren<SpawnPointsRegistry>(true);
            if (registry != null) break;
        }

        if (registry != null && registry.TryGet(spawnId, out var spawn))
        {
            player.position = spawn.position;
            return true;
        }
        else
            Debug.LogWarning($"Spawn '{spawnId}' nao encontrado na cena '{SceneManager.GetActiveScene().name}'.");

        return false;
    }

    private void SnapCameraToPlayer(Vector3 previousPosition, Vector3 newPosition)
    {
        if (virtualCamera == null)
        {
            Debug.LogWarning("Cinemachine virtual camera reference not set on SceneTransitionController.");
            return;
        }

        virtualCamera.OnTargetObjectWarped(player, newPosition - previousPosition);
    }

    public void LoadInitialMap(string sceneName, string spawnId)
    {
        // Metodo opcional pra carregar o primeiro mapa no Start
        requestSceneLoad.Raise(new SceneLoadRequest { sceneName = sceneName, spawnId = spawnId });
    }
}
