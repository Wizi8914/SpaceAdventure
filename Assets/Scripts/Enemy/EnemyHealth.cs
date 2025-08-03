using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float health = 100f;

    [HideInInspector] public AIAgent agent; // Reference to the AI agent
    [HideInInspector] public bool isDead;
    // Time before the enemy is destroyed after death
    [SerializeField] public SphereCollider headCollider;
    [SerializeField] public Canvas canvas; // UI Slider to display health
    private Slider healthBar;
    private HealBarCut healBarCut;
    

    void Start()
    {
        agent = GetComponent<AIAgent>();
        healthBar = canvas.GetComponentInChildren<Slider>();
        healBarCut = healthBar.GetComponent<HealBarCut>();
        agent.config.maxHealth = health; // Set the max health in the agent config
        healthBar.maxValue = health; // Initialize the health bar value
        healthBar.value = health; // Set the initial health bar value

        UpdateHealthBar();
    }

    public void TakeDamage(float damage, bool isHeadShot = false)
    {
        if (health > 0f)
        {
            float beforeDamageFillAmount = healthBar.normalizedValue;
            health -= damage;
            if (health <= 0f) EnemyDeath();
            else
            {
                UpdateHealthBar();
                healBarCut.UpdateHealBar(beforeDamageFillAmount, healthBar.normalizedValue, isHeadShot);
            }  
        }
    }

    void EnemyDeath()
    {

        agent.stateMachine.ChangeState(AIStateID.Death);
        canvas.enabled = false; // Disable the health bar canvas
        Destroy(gameObject, agent.config.TimeToDie);
    }
    
    void UpdateHealthBar()
    {
        healthBar.value = health;
    }
}
