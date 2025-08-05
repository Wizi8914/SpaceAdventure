using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : Health
{
    private RagdollManager ragdollManager;
    private Canvas canvas; // UI Slider to display health
    private Slider healthBar;
    private HealBarCut healBarCut;


    private float displayedHealth;

    protected override void OnStart()
    {
        canvas = GameManager.Instance.canvas;
        healthBar = canvas.GetComponentInChildren<Slider>();
        healBarCut = healthBar.GetComponent<HealBarCut>();

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        displayedHealth = currentHealth; // Initialize displayed health

        ragdollManager = GetComponent<RagdollManager>();
    }

    protected override void OnDamage(float damage, bool isHeadShot = false)
    {
        if (isDead) return;

        float beforeDamageFillAmount = healthBar.normalizedValue;
        displayedHealth -= damage;

        if (displayedHealth >= 0f)
        {
            healthBar.value = currentHealth;
            healBarCut.UpdateHealBar(beforeDamageFillAmount, healthBar.normalizedValue);
        }
    }

    protected override void OnHeal(float amount)
    {
        healthBar.value = currentHealth;
    }

    protected override void OnDeath()
    {
        isDead = true;
        GetComponent<Animator>().enabled = false;
        ragdollManager.EnableRagdoll();
    }
}
