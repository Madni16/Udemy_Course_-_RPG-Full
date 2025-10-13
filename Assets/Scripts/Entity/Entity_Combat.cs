using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    Entity_VFX vfx;
    public float damage = 10f;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask targetLayer;

    private void Awake()
    {
        vfx = GetComponent<Entity_VFX>();
    }

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamageable damageable = target.GetComponent<IDamageable>();

            if(damageable == null)
                continue;

            damageable.TakeDamage(damage, transform);
            vfx.CreateOnHitVFX(target.transform);
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
