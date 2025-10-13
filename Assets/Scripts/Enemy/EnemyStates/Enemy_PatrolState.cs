public class Enemy_PatrolState : EnemyState
{
    public Enemy_PatrolState(Enemy enemy, StateMachine statemachine, string animBoolName) : base(enemy, statemachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.BattleState);        
    }
}
