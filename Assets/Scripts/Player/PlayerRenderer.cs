using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRenderer : MonoBehaviour
{


    //[Header("Dependencies")]
    //public SpriteRenderer spriteRenderer;

    //public void OnMovement(InputAction.CallbackContext value)
    //{
    //    Vector2 movementInput = value.ReadValue<Vector2>();

    //    if(movementInput.x > 0f && PlayerIsLookingLeft()) // moving to the right but currently looking left
    //    {
    //        spriteRenderer.flipX = false;
    //    }
    //    else if(movementInput.x < 0f && !PlayerIsLookingLeft()) // moving to the left but currently looking right
    //    {
    //        spriteRenderer.flipX = true;
    //    }
    //}

    //private bool PlayerIsLookingLeft()
    //{
    //    return spriteRenderer.flipX;
    //}
}
