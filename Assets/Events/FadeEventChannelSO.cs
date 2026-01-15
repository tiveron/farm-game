using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/Fade Channel")]
public class FadeEventChannelSO : ScriptableObject
{
    public event Action<float> OnRaised;
    public void Raise(float duration) => OnRaised?.Invoke(duration);
}
