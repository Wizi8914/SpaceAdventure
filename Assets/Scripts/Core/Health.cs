using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;

    public float invincibilityDuration = 0f;
    private float invincibilityTimer = 0f;

    void Update()
    {
        if (invincibilityTimer >= 0f) invincibilityTimer -= Time.deltaTime;
    }

    public bool IsInvincible()
    {
        return invincibilityTimer >= 0f;
    }

    [HideInInspector] public bool isDead = false; // Flag to check if the entity is dead

    void Start()
    {
        currentHealth = maxHealth;
        OnStart();
    }

    public void TakeDamage(float damage, bool isHeadShot = false, GameObject emitter = null)
    {
        if (isDead) return;
        if (IsInvincible()) return;

        invincibilityTimer = invincibilityDuration;

        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die(emitter);
        }
        OnDamage(damage, isHeadShot);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        OnHeal(amount);
    }

    private void Die(GameObject killer = null)
    {
        OnDeath(killer);
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnDeath(GameObject killer)
    {

    }

    protected virtual void OnDamage(float damage = 0f, bool isHeadShot = false)
    {

    }

    protected virtual void OnHeal(float amount)
    {

    }
}