using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "AIAgentConfig", menuName = "AI/AIAgentConfig")]
public class AIAgentConfig : ScriptableObject
{
    [Header("AI Agent Configuration")]
    public float maxHealth = 100f;

    [Header("AI Behavior Settings")]
    public float maxTime = 1f;
    public float maxDistance = 1f;
    public float minSeenTime = 1f; // Minimum time the player must be seen to trigger a response
    public float maxSightDistance = 10f; // Maximum distance for AI to see the player

    [Header("Detection Settings")]
    public float minAttackDistance = 2f; // Minimum distance for the AI to start attacking
    public float maxAttackDistance = 5f; // Maximum distance for the AI to consider
    public float maxChaseDistance = 10f; // Maximum distance for the AI to chase the player
    public float maxChaseTime = 5f; // Maximum time the AI will chase the player
    public float maxChaseDurationWithoutVision = 3f; // Maximum time the AI will chase the player without seeing them

    [Header("Weapon Settings")]
    public GameObject weaponPrefab; //
    public float inaccuracy = 2f; // How much the bullet can deviate from the target direction

    [Header("Death Settings")]
    public float TimeToDie = 5f; // Time before the enemy is destroyed after death
}
