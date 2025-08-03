using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int currentHealth = 100; // Player's health
    public int maxHealth = 100; // Maximum health the player can have
    [SerializeField] public Canvas canvas; // UI Slider to display health
    private Slider healthBar;
    private HealBarCut healBarCut;

    [HideInInspector] public bool isDead = false; // Flag to check if the player is dead

    void Start()
    {
        healthBar = canvas.GetComponentInChildren<Slider>();
        healBarCut = healthBar.GetComponent<HealBarCut>();
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0f)
        {
            float beforeDamageFillAmount = healthBar.normalizedValue;
            currentHealth -= damage;
            if (currentHealth <= 0f) IsDead();
            else
            {
                healthBar.value = currentHealth;
                healBarCut.UpdateHealBar(beforeDamageFillAmount, healthBar.normalizedValue);
            }  
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }


}
