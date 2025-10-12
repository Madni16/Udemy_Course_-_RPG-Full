public class Player_AirborneState : PlayerState
{
    public Player_AirborneState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.airSpeedMultiplier), rb.linearVelocity.y);

        if (input.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.JumpAttackState);
    }
}
