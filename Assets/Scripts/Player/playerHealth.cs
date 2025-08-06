using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    private RagdollManager ragdollManager;
    private Canvas canvas; // UI Slider to display health
    private Slider healthBar;
    private HealBarCut healBarCut;
    private VignetteEffect vignetteEffect;

    private CameraManager cameraManager; // Reference to the camera manager

    private float displayedHealth;

    public float invincibilityDur = 0.4f; // Duration of invincibility after taking damage

    protected override void OnStart()
    {
        canvas = GameManager.Instance.canvas;
        healthBar = canvas.GetComponentInChildren<Slider>();
        healBarCut = healthBar.GetComponent<HealBarCut>();
        cameraManager = FindAnyObjectByType<CameraManager>(); // Initialize camera manager

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        vignetteEffect = GetComponent<VignetteEffect>();
        displayedHealth = currentHealth; // Initialize displayed health

        ragdollManager = GetComponent<RagdollManager>();
        invincibilityDuration = invincibilityDur;
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

        vignetteEffect.SetVignetteIntensity(1f - healthBar.normalizedValue);
        
    }

    protected override void OnHeal(float amount)
    {
        healthBar.value = currentHealth;
    }

    protected override void OnDeath(GameObject killer)
    {
        isDead = true;
        GetComponent<Animator>().enabled = false;
        ragdollManager.EnableRagdoll();


        WeaponClassManager weaponClassManager = GetComponent<WeaponClassManager>();
        weaponClassManager.DropWeapon();
        weaponClassManager.weapon[weaponClassManager.currentWeaponIndex].GetComponent<WeaponManager>().enabled = false; // Disable the current weapon manager
        weaponClassManager.enabled = false; // Disable the weapon class manager

        GetComponent<MovementStateManager>().enabled = false; // Disable movement
        GetComponent<AimStateManager>().enabled = false; // Disable weapon management

        cameraManager.EnableKillCam(killer.transform);
    }
}
