using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    private Enemy_VFX vfx;
    public Enemy_StunnedState(Enemy enemy, StateMachine statemachine, string animBoolName) : base(enemy, statemachine, animBoolName)
    {
        vfx = enemy.GetComponent<Enemy_VFX>();
    }

    public override void Enter()
    {
        base.Enter();

        vfx.EnableEnemyAlert(false);
        enemy.EnableCounterWindow(false);

        stateTimer = enemy.stunnedDuration;
        rb.linearVelocity = new Vector2(enemy.stunnedVelocity.x * -enemy.facingDir, enemy.stunnedVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.IdleState);
    }
}
