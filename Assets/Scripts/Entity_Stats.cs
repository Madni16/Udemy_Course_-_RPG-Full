using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHealth;
    public Stat vitality; // Each point gives +5 HP;

    public float GetMaxHealth()
    {
        float baseHealth = maxHealth.GetValue();
        float bonusHealth = vitality.GetValue() * 5;

        return baseHealth + bonusHealth;
    }
}
