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

        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.JumpState);

        if (input.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.BasicAttackState);

        if (input.Player.CounterAttack.WasPressedThisFrame())
            stateMachine.ChangeState(player.CounterAttackState);
    }
}
