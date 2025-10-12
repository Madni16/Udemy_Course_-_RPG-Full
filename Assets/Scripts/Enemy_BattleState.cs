using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    public Enemy_BattleState(Enemy enemy, StateMachine statemachine, string animBoolName) : base(enemy, statemachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("I entered Battle State");
    }
}
