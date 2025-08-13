using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "AIAgentConfig", menuName = "AI/AIAgentConfig")]
public class AIAgentConfig : ScriptableObject
{
    [Header("AI Agent Configuration")]
    [Tooltip("Maximum health points for the AI agent")]
    public float maxHealth = 100f;
    [Tooltip("Movement speed of the AI agent")]
    public float speed = 3.5f;

    [Header("AI Behavior Settings")]
    [Tooltip("Maximum time for AI behavior calculations")]
    public float maxTime = 1f;
    [Tooltip("Maximum distance for AI behavior triggers")]
    public float maxDistance = 1f;
    [Tooltip("Minimum time the player must be seen to trigger a response")]
    public float minSeenTime = 1f;
    [Tooltip("Maximum distance for AI to see the player")]
    public float maxSightDistance = 10f;

    [Header("Patrol Parameters")]
    [Tooltip("Distance to the next patrol point before moving to the next one")]
    public float patrolDistanceThreshold = 1f;
    [Tooltip("Time to wait at each patrol point")]
    public float patrolWaitTime = 0f;
    [Tooltip("Movement speed during patrol")]
    public float patrolSpeed = 3.5f;

    [Header("Detection Settings")]
    [Tooltip("Distance the AI can see")]
    public float lookDistance = 30f;
    [Tooltip("Field of view angle for the AI")]
    [Range(0, 360)]
    public float fieldOfView = 120f;

    [Tooltip("Minimum distance for the AI to start attacking")]
    public float minAttackDistance = 2f;
    [Tooltip("Maximum distance for the AI to consider attacking")]
    public float maxAttackDistance = 5f;

    [Header("Chase Settings")]
    [Tooltip("Maximum distance for the AI to chase the player")]
    public float maxChaseDistance = 10f;
    [Tooltip("Maximum time the AI will chase the player")]
    public float maxChaseTime = 5f;
    [Tooltip("Maximum time the AI will chase the player without seeing them")]
    public float maxChaseDurationWithoutVision = 3f;

    [Header("Weapon Settings")]
    [Tooltip("Weapon prefab to spawn for this AI agent")]
    public GameObject weaponPrefab;
    [Tooltip("How much the bullet can deviate from the target direction")]
    public float inaccuracy = 2f;

    [Header("Death Settings")]
    [Tooltip("Time before the enemy is destroyed after death")]
    public float TimeToDie = 5f;
}
