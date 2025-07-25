using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float health = 100f;
    RagdollManager ragdollManager;
    [HideInInspector] public bool isDead;
    [SerializeField] private float timeToDie = 10f; // Time before the enemy is destroyed after death
    [SerializeField] public SphereCollider headCollider;
    [SerializeField] public Canvas canvas; // UI Slider to display health
    private Slider healthBar;

    void Start()
    {
        ragdollManager = GetComponent<RagdollManager>();
        healthBar = canvas.GetComponentInChildren<Slider>();
        healthBar.maxValue = health;
        UpdateHealthBar();
        
    }

    public void TakeDamage(float damage)
    {
        if (health > 0f)
        {
            health -= damage;
            if (health <= 0f) EnemyDeath();
            else UpdateHealthBar();
        }
    }

    void EnemyDeath()
    {
        ragdollManager.EnableRagdoll();
        canvas.enabled = false; // Disable the health bar canvas
        Destroy(gameObject, timeToDie); // Destroy the enemy game object after a delay
    }
    
    void UpdateHealthBar()
    {
        healthBar.value = health;
    }
}
