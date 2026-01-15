using UnityEngine;

public class PlayerInputGate : MonoBehaviour
{
    [SerializeField] private Behaviour[] componentsToDisable;

    public void SetEnabled(bool enabled)
    {
        foreach (var c in componentsToDisable)
            if (c) c.enabled = enabled;
    }
}
