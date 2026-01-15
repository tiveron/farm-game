using System;
using UnityEngine;

[System.Serializable]
public struct SceneLoadRequest
{
    public string sceneName;
    public string spawnId;
}

[CreateAssetMenu(menuName = "Game/Events/Scene Load Channel")]
public class SceneLoadEventChannelSO : ScriptableObject
{
    public event Action<SceneLoadRequest> OnRequested;
    public void Raise(SceneLoadRequest request) => OnRequested?.Invoke(request);
}
