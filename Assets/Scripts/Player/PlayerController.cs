using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D rigidbody;

    // Private variables
    private Vector2 _movementInput;

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = _movementInput * speed;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }

}
