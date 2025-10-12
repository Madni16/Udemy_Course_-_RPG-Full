public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0 && !player.groundDetected)
            stateMachine.ChangeState(player.FallState);

        if (input.Player.Jump.WasPerformedThisFrame())
            stateMachine.ChangeState(player.JumpState);

        if (input.Player.Attack.WasPerformedThisFrame())
            stateMachine.ChangeState(player.BasicAttackState);
    }
}
