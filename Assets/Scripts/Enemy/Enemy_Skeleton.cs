public class Enemy_Skeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_IdleState(this, stateMachine, "idle");
        MoveState = new Enemy_MoveState(this, stateMachine, "move");
        AttackState = new Enemy_AttackState(this, stateMachine, "attack");
        BattleState = new Enemy_BattleState(this, stateMachine, "battle");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState);
    }
}
