using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputSet input;
    private StateMachine stateMachine;

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }

    public Vector2 moveInput { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        input = new PlayerInputSet();

        IdleState = new Player_IdleState(this, stateMachine, "Idle");
        MoveState = new Player_MoveState(this, stateMachine, "Move");
    }

    private void OnEnable()
    {
        input.Enable();


        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
