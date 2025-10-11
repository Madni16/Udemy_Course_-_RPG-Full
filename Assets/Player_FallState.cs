public class Player_FallState : Player_AirborneState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.groundDetected)
            stateMachine.ChangeState(player.IdleState);

        if (player.wallDetected && player.moveInput.x != 0)
            stateMachine.ChangeState(player.WallSlideState);
    }
}
