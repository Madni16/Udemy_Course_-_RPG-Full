using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationTriggers
{
    private Enemy enemy;
    private Enemy_VFX enemyVfx;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemyVfx = GetComponentInParent<Enemy_VFX>();
    }

    private void EnableCounterWindow()
    {
        enemyVfx.EnableEnemyAlert(true);
        enemy.EnableCounterWindow(true);
    }

    private void DisableCounterWindow()
    {
        enemyVfx.EnableEnemyAlert(false);
        enemy.EnableCounterWindow(false);
    }
}
