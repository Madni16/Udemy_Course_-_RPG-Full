using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX vfx;
    private Entity_Stats stats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask targetLayer;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamageable damageable = target.GetComponent<IDamageable>();

            if (damageable == null)
                continue;

            float elementalDamage = stats.GetElementalDamage(out ElementType element);
            float damage = stats.GetPhysicalDamage(out bool isCrit);

            bool targetGotHit = damageable.TakeDamage(damage, elementalDamage, element, transform);

            if (targetGotHit)
                vfx.CreateOnHitVFX(target.transform, isCrit);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, targetLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
