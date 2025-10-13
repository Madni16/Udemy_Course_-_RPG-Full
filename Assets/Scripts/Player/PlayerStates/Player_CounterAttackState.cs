using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat combat;
    private bool counterSuccessful;
    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = combat.GetCounterRecoveryDuration();
        counterSuccessful = combat.CounterAttackPerformed();

        anim.SetBool("counterAttackPerformed", counterSuccessful);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, rb.linearVelocity.y);

        if(triggerCalled)
            stateMachine.ChangeState(player.IdleState);

        if(stateTimer < 0 && !counterSuccessful)
            stateMachine.ChangeState(player.IdleState);
    }
}
