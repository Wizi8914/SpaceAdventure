using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    [HideInInspector] public AIAgent agent; // Reference to the AI agent
    // Time before the enemy is destroyed after death
    [SerializeField] public SphereCollider headCollider;
    [SerializeField] public Canvas canvas; // UI Slider to display health
    private Slider healthBar;
    private HealBarCut healBarCut;

    private float displayedHealth;

    protected override void OnStart()
    {
        agent = GetComponent<AIAgent>();
        healthBar = canvas.GetComponentInChildren<Slider>();

        healBarCut = healthBar.GetComponent<HealBarCut>();
        maxHealth = agent.config.maxHealth; // Set the max health in the agent config
        healthBar.maxValue = currentHealth; // Initialize the health bar value
        healthBar.value = currentHealth; // Set the initial health bar value
        displayedHealth = currentHealth; // Initialize displayed health

        UpdateHealthBar();
    }

    protected override void OnDeath(GameObject killer)
    {
        isDead = true;
        GetComponent<Animator>().enabled = false; // Disable the animator
        agent.ragdollManager.EnableRagdoll(); // Enable ragdoll physics

        // Change the AI state to death
        agent.navMeshAgent.enabled = false; // Disable navigation agent
        agent.animator.enabled = false; // Disable animator to stop animations

        // Notify the AI state machine of death
        agent.stateMachine.ChangeState(AIStateID.Death);
        canvas.enabled = false; // Disable the health bar canvas
        Destroy(gameObject, agent.config.TimeToDie); // Destroy the enemy after a delay
    }

    protected override void OnDamage(float damage, bool isHeadShot = false)
    {
        if (isDead) return;

        float beforeDamageFillAmount = healthBar.normalizedValue;
        displayedHealth -= damage;

        if (displayedHealth >= 0f)
        {
            UpdateHealthBar();
            healBarCut.UpdateHealBar(beforeDamageFillAmount, healthBar.normalizedValue, isHeadShot);
        }

        if (agent.stateMachine.currentState == AIStateID.Idle || agent.stateMachine.currentState == AIStateID.Patrol)
        {
            agent.stateMachine.ChangeState(AIStateID.Attack);
        }
    }
    
    void UpdateHealthBar()
    {
        healthBar.value = currentHealth;
    }
}
