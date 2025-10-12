using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState IdleState;
    public Enemy_MoveState MoveState;

    [Header("Movement details")]
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;
    [Range(0f, 2f)]
    public float moveAnimSpeedMultiplier = 1f;
}
