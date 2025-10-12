using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public PlayerInputSet input { get; private set; }

    public Player_IdleState IdleState { get; private set; }
    public Player_MoveState MoveState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public Player_WallSlideState WallSlideState { get; private set; }
    public Player_WallJumpState WallJumpState { get; private set; }
    public Player_DashState DashState { get; private set; }
    public Player_BasicAttackState BasicAttackState { get; private set; }
    public Player_JumpAttackState JumpAttackState { get; private set; }

    [Header("Attack details")]
    public Vector2[] attackVelocity;
    public Vector2 jumpAttackVelocity;
    public float attackVelocityDuration = .1f;
    public float comboWindowDuration = .5f;
    private Coroutine queuedAttackCoroutine;

    [Header("Movement details")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public Vector2 wallJumpForce;
    [Range(0, 1)]
    public float airSpeedMultiplier = 0.8f;
    [Range(0, 1)]
    public float wallSlideMultiplier = 0.3f;
    [Space]
    public float dashDuration = .25f;
    public float dashSpeed = 20f;

    public Vector2 moveInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        input = new PlayerInputSet();

        IdleState = new Player_IdleState(this, stateMachine, "idle");
        MoveState = new Player_MoveState(this, stateMachine, "move");
        JumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        FallState = new Player_FallState(this, stateMachine, "jumpFall");
        WallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        WallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        DashState = new Player_DashState(this, stateMachine, "dash");
        BasicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        JumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }

    public void EnterAttackStateWithDelay()
    {
        if (queuedAttackCoroutine != null)
            StopCoroutine(queuedAttackCoroutine);

        queuedAttackCoroutine = StartCoroutine(EnterAttackStateWithDelayCoroutine());
    }

    private IEnumerator EnterAttackStateWithDelayCoroutine()
    {
        yield return new WaitForEndOfFrame();
        stateMachine.ChangeState(BasicAttackState);
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
}
