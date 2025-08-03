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

    [Header("Weapon Settings")]
    public GameObject weaponPrefab; //

    [Header("Death Settings")]
    public float TimeToDie = 5f; // Time before the enemy is destroyed after death
}
