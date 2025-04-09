using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    public IPlayerState CurrentState;
    public IPlayerState PreviousState;

    [HideInInspector] public Vector2 moveInput;
    [HideInInspector] public Animator animator;
    [HideInInspector] public CharacterController controller;

    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.8f;
    public Vector3 velocity;

    [HideInInspector] public bool attackPressed;
    [HideInInspector] public bool jumpPressed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        ChangeState(new PlayerIdleState());
    }

    void Update()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -1f;
        }

        controller.Move(velocity * Time.deltaTime);

        if (CurrentState != null)
            CurrentState.Execute(this);
    }

    public void ChangeState(IPlayerState newState)
    {
        if (CurrentState != null)
            CurrentState.Exit(this);

        PreviousState = CurrentState;
        CurrentState = newState;

        if (CurrentState != null)
            CurrentState.Enter(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpPressed = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            attackPressed = true;
    }
}