using UnityEngine;

public class InitialMapLoader : MonoBehaviour
{
    [SerializeField] private SceneTransitionController controller;
    [SerializeField] private string firstScene = "Farm";
    [SerializeField] private string firstSpawnId = "default";

    private void Start()
    {
        controller.LoadInitialMap(firstScene, firstSpawnId);
    }
}