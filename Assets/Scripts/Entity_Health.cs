using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entityVfx;

    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected bool isDead;

    protected virtual void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        entityVfx?.PlayOnDamageVfx();
        ReduceHealth(damage);
    }

    protected void ReduceHealth(float damage)
    {
        maxHealth -= damage;

        if (maxHealth <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Entity died!");
    }
}
