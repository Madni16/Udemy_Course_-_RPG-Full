using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    public float GetElementalDamage(out ElementType element)
    {
        float fireDamage = offense.fireDamage.GetValue();
        float iceDamage = offense.iceDamage.GetValue();
        float lightningDamage = offense.lightningDamage.GetValue();

        float bonusElementalDamage = major.intelligence.GetValue(); // Each point of Intelligence gives 1 damage

        float highestDamage = fireDamage;
        element = ElementType.Fire;

        if (iceDamage > highestDamage)
        {
            highestDamage = iceDamage;
            element = ElementType.Ice;
        }

        if (lightningDamage > highestDamage)
        {
            highestDamage = lightningDamage;
            element = ElementType.Lightning;
        }

        if (highestDamage <= 0)
        {
            element = ElementType.None;
            return 0;
        }

        // If an element is not highest damage, use 50% of the damage and add to the total damage.
        float bonusFire = (fireDamage == highestDamage) ? 0 : fireDamage * .5f;
        float bonusIce = (iceDamage == highestDamage) ? 0 : iceDamage * .5f;
        float bonusLightning = (lightningDamage == highestDamage) ? 0 : lightningDamage * .5f;

        float weakerElementsDamage = bonusFire + bonusIce + bonusLightning;
        float finalDamage = highestDamage + weakerElementsDamage + bonusElementalDamage;

        return finalDamage;
    }

    public float GetElementalResistance(ElementType element)
    {
        float baseResistance = 0;
        float bonusResistance = major.intelligence.GetValue() * .5f; // Each point of intelligence gives 0.5 elemental resistance

        switch (element)
        {
            case ElementType.Fire:
                baseResistance = defense.fireRes.GetValue();
                break;
            case ElementType.Ice:
                baseResistance = defense.iceRes.GetValue();
                break;
            case ElementType.Lightning:
                baseResistance = defense.lightningRes.GetValue();
                break;
        }

        float resistance = baseResistance + bonusResistance;
        float resistanceCap = 75f; // Elemental resistance will be capped at 75%
        float finalResistance = Mathf.Clamp(resistance, 0, resistanceCap) / 100; // Convert value into 0 to 1 multiplier by dividing with 100

        return finalResistance;
    }

    public float GetPhysicalDamage(out bool isCrit)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamage = major.strength.GetValue(); // Each point of Strength gives 1 damage
        float totalBaseDamage = baseDamage + bonusDamage;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f; // Each point of Agility gives 0.3% crit chance
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * .5f; // Each point of Strength gives 0.5% crit power
        float critPower = (baseCritPower + bonusCritPower) / 100; // Total crit power as multiplier (e.g. 150 / 100 = 1.5f -- 150% crit power as 1.5 multiplier)

        isCrit = Random.Range(1, 100) <= critChance;
        float finalDamage = isCrit ? totalBaseDamage * critPower : totalBaseDamage;

        return finalDamage;
    }

    public float GetArmorPenetration()
    {
        // Total armor penetration as multiplier (e.g. 30 / 100 = 0.3f multiplier -- 30% armor penetration)
        float finalPenetration = offense.armorPenetration.GetValue() / 100;

        return finalPenetration;
    }

    public float GetArmorMitigation(float armorPenetration)
    {
        float baseArmor = defense.armor.GetValue();
        float bonusArmor = major.vitality.GetValue(); // Each point of Vitality gives 1 armor.
        float totalArmor = baseArmor + bonusArmor;

        float penetrationMultiplier = Mathf.Clamp01(1 - armorPenetration); // e.g. 1 - .4f = .6f -- 60% of armor will be used for calculation.
        float effectiveArmor = totalArmor * penetrationMultiplier;

        float mitigation = effectiveArmor / (effectiveArmor + 100);
        float mitigationCap = .85f; // Max mitigation will be capped at 85%

        float finalMitigation = Mathf.Clamp(mitigation, 0, mitigationCap);

        return finalMitigation;
    }


    public float GetEvasion()
    {
        float baseEvasion = defense.evasion.GetValue();
        float bonusEvasion = major.agility.GetValue() * .5f; // Each point of Agility gives you 0.5% evasion

        float totalEvasion = baseEvasion + bonusEvasion;
        float evasionCap = 85f; // Evasion will be capped at 85%

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap);

        return finalEvasion;
    }
    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHealth.GetValue();
        float bonusMaxHealth = major.vitality.GetValue() * 5; // Each point of Vitality gives you 5 HP

        float finalMaxHealth = baseMaxHealth + bonusMaxHealth;
        return finalMaxHealth;
    }
}
