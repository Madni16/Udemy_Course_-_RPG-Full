public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        HandleWallSlide();

        if (input.Player.Jump.WasPerformedThisFrame())
            stateMachine.ChangeState(player.WallJumpState);

        if (!player.wallDetected)
            stateMachine.ChangeState(player.FallState);

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.IdleState);

            if(player.facingDir != player.moveInput.x)
                player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        else
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideMultiplier);
    }
}
