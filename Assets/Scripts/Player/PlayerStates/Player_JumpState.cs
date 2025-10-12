public class Player_JumpState : Player_AirborneState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        // We need to be sure we are not in jump attack state when we transfer to fall state
        if (rb.linearVelocity.y < 0 && stateMachine.currentState != player.JumpAttackState)
            stateMachine.ChangeState(player.FallState);
    }
}
