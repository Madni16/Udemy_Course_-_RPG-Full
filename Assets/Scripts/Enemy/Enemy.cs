using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState IdleState;
    public Enemy_MoveState MoveState;
    public Enemy_AttackState AttackState;
    public Enemy_BattleState BattleState;
    public Enemy_DeadState DeadState;
    public Enemy_StunnedState StunnedState;

    private Enemy_VFX vfx;

    [Header("Battle details")]
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2f;
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 1f;
    public Vector2 retreatVelocity;

    [Header("Stunned state details")]
    public float stunnedDuration = 1f;
    public Vector2 stunnedVelocity = new Vector2(7f, 7f);
    [SerializeField] protected bool canBeStunned;

    [Header("Movement details")]
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;
    [Range(0f, 2f)]
    public float moveAnimSpeedMultiplier = 1f;

    [Header("Player detection")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float playerCheckDistance = 10f;
    public Transform player { get; private set; }
    public float playerDetectionAlertDuration = 0.3f;

    protected override void Awake()
    {
        base.Awake();

        vfx = GetComponent<Enemy_VFX>();
    }

    public void EnableCounterWindow(bool enable) => canBeStunned = enable;

    public void ToggleEnemyDetectionAlert(bool enable) => vfx.EnableEnemyAlert(enable);

    public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(DeadState);
    }

    private void HandlePlayerDeath()
    {
        stateMachine.ChangeState(IdleState);
    }

    public void TryEnterBattleState(Transform player)
    {
        if (stateMachine.currentState == BattleState || stateMachine.currentState == AttackState)
            return;

        this.player = player;
        ToggleEnemyDetectionAlert(true);
        stateMachine.ChangeState(BattleState);
    }

    public Transform GetPlayerReference()
    {
        if (player == null)
            player = PlayerDetected().transform;

        return player;
    }

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit =
            Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, playerLayer | groundLayer);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * playerCheckDistance), playerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * attackDistance), playerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + (facingDir * minRetreatDistance), playerCheck.position.y));
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath;
    }
}
