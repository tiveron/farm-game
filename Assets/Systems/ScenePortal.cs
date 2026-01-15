using UnityEngine;

public class ScenePortal : MonoBehaviour
{
    [Header("Destination")]
    [SerializeField] private string destinationSceneName;
    [SerializeField] private string destinationSpawnId;

    [Header("Events")]
    [SerializeField] private SceneLoadEventChannelSO requestSceneLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("heeeere");
        Debug.Log("destinationSceneName");
        Debug.Log("destinationSpawnId");
        Debug.Log("destinationSceneName");
        Debug.Log(!other.CompareTag("Player"));

        if (!other.CompareTag("Player")) return;

        requestSceneLoad.Raise(new SceneLoadRequest
        {
            sceneName = destinationSceneName,
            spawnId = destinationSpawnId
        });
    }
}
