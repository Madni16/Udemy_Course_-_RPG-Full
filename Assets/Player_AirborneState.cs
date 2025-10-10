using UnityEngine;

public class Player_AirborneState : EntityState
{
    private float airDragMultiplier = 0.99f;
    public Player_AirborneState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        HandleAirMovement();
    }

    // I freestyled a little here. I created air deceleration and a minimum air speed to give the player more control and make air movement feel more fluent
    private void HandleAirMovement()
    {
        if (player.moveInput.x != 0)
        {
            float absVelocityX = Mathf.Abs(rb.linearVelocity.x);

            if (absVelocityX < player.minAirSpeed)
                player.SetVelocity(player.moveInput.x * player.minAirSpeed, rb.linearVelocity.y);
            else
                player.SetVelocity(player.moveInput.x * absVelocityX, rb.linearVelocity.y);
        }
        else
        {
            player.SetVelocity(rb.linearVelocity.x * airDragMultiplier, rb.linearVelocity.y);
        }
    }
}
