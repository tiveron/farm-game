using UnityEngine;
using UnityEngine.InputSystem;

public enum FacingDir { Up, Down, Left, Right }

public class PlayerAnimation : MonoBehaviour
{

    [Header("Dependencies")]
    public Animator animator;

    public Vector2 MoveInput { get; private set; }
    public FacingDir Facing { get; private set; } = FacingDir.Down;

    public void OnMovement(InputAction.CallbackContext value)
    {

        MoveInput = value.ReadValue<Vector2>();
        float x = 0f;
        float y = 0f;
        //Debug.Log(MoveInput);

        // Zera quando solta
        if (value.canceled)
            MoveInput = Vector2.zero;

        // Atualiza direção apenas se estiver se movendo
        if (MoveInput.sqrMagnitude > 0.0001f)
        {
            Facing = ToFacing4(MoveInput);
            //Debug.Log(Facing);
            animator.SetBool("IsMoving", true);

            if (Facing  == FacingDir.Up)
            {
                x = 0f;
                y = 1f;
            } else if (Facing == FacingDir.Down)
            {
                x = 0f;
                y = -1f;
            } else if (Facing == FacingDir.Left)
            {
                x = -1f;
                y = 0f;
            } else if (Facing == FacingDir.Right)
            {
                x = 1f;
                y = 0f;
            }

            animator.SetFloat("MoveX", x);
            animator.SetFloat("MoveY", y);
            animator.SetFloat("LastMoveX", x);
            animator.SetFloat("LastMoveY", y);

        } else
        {
            animator.SetBool("IsMoving", false);

        }
    }

    private static FacingDir ToFacing4(Vector2 v)
    {
        Debug.Log(v);
        // Decide pela maior componente (horizontal vs vertical)
        if (Mathf.Abs(v.x) < Mathf.Abs(v.y))
            return v.y >= 0 ? FacingDir.Up : FacingDir.Down;
        else
            return v.x >= 0 ? FacingDir.Right : FacingDir.Left;
    }
}