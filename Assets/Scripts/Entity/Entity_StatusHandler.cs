using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;
    private Entity_Stats stats;
    private ElementType currentEffect = ElementType.None;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectCoroutine(reducedDuration, slowMultiplier));
    }

    private IEnumerator ChilledEffectCoroutine(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        entityVfx.PlayOnStatusVfx(duration, currentEffect);

        yield return new WaitForSeconds(duration);
        currentEffect = ElementType.None;
    }

    public bool CanBeApplied(ElementType element)
    {
        return currentEffect == ElementType.None;
    }
}
