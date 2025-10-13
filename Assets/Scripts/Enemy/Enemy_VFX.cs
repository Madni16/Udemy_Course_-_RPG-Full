using UnityEngine;

public class Enemy_VFX : Entity_VFX
{
    [Header("Enemy Alert")]
    [SerializeField] private GameObject enemyAlert;

    public void EnableEnemyAlert(bool enable) => enemyAlert.SetActive(enable);
}
