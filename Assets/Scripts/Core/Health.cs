using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 100f; // Current health of the entity
    public float maxHealth = 100f; // Maximum health of the entity

    [HideInInspector] public bool isDead = false; // Flag to check if the entity is dead

    void Start()
    {
        currentHealth = maxHealth;
        OnStart();
    }

    public void TakeDamage(float damage, bool isHeadShot = false)
    {
        if (isDead) return; // If already dead, do nothing

        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
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

    private void Die()
    {
        OnDeath();
    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnDeath()
    {

    }

    protected virtual void OnDamage(float damage = 0f, bool isHeadShot = false)
    {

    }

    protected virtual void OnHeal(float amount)
    {

    }
}