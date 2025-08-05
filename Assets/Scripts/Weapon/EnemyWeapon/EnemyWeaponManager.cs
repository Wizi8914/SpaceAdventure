using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnemyWeaponManager : MonoBehaviour
{   
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAutomatic = true;

    [Header("Burst Fire")]
    [SerializeField] bool isBurstFire = false;
    [SerializeField] float burstInterval = 0.08f;
    bool isBursting = false;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform bulletSpawnLocation;
    [SerializeField] int bulletsPerShot;
    [SerializeField] float bulletVelocity;
    [SerializeField] float bulletHoleSizeMultiplier;
    public float damage = 10;

    public float inaccuracy = 2; // How much the bullet can deviate from the target direction

    [SerializeField] AudioClip fireSound;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public WeaponAmmo ammo;
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticle;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20f;

    public Transform leftHandTarget, leftHandHint;


    void Start()
    {
        ammo = GetComponent<WeaponAmmo>();
        audioSource = GetComponent<AudioSource>();
        bulletSpawnLocation = transform.Find("BulletSpawnPos");
        muzzleFlashLight = bulletSpawnLocation.GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0f;
        muzzleFlashParticle = bulletSpawnLocation.GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    
    void Update()
    {

        // Debug.Log($"Fire Rate Timer: {fireRateTimer}, Fire Rate: {fireRate}");
        // Debug.Log($"Delta time: {Time.deltaTime}");

        /*
        if (isBurstFire && !isBursting)
        {
            StartCoroutine(BurstFire());
        }
        else
        {
            Fire();
        }
        */

        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0f, lightReturnSpeed * Time.deltaTime);
    }
    
    public bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;

        if (ammo.currentAmmo <= 0) return false;
        if (fireRateTimer < fireRate) return false;

        var playerHealth = GameManager.Instance.player.GetComponent<playerHealth>();
        if (playerHealth != null && playerHealth.isDead) return false;

        return true;
    }

    public void Fire(Transform target = null)
    {
        fireRateTimer = 0f;
        bulletSpawnLocation.LookAt(target ? target.position : GameManager.Instance.player.transform.position);
        bulletSpawnLocation.localEulerAngles = new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy), 0);

        // Play fire sound
        audioSource.pitch = Random.Range(0.85f, 1.1f);
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(fireSound);

        TriggerMuzzleFlash();

        ammo.currentAmmo--;

        for (int i = 0; i < (isBurstFire ? 1 : bulletsPerShot); i++)
        {

            GameObject currentBullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
            EnemyBullet bullet = currentBullet.GetComponent<EnemyBullet>();

            bullet.holeSizeMultiplier = bulletHoleSizeMultiplier;

            bullet.weapon = this; // Assign the weapon to the bullet

            bullet.direction = bulletSpawnLocation.transform.forward;
            bullet.parentObject = gameObject;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

            if (rb != null) rb.AddForce(bulletSpawnLocation.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    IEnumerator BurstFire()
    {
        isBursting = true;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            if (ammo.currentAmmo == 0) break;

            Fire();
            yield return new WaitForSeconds(burstInterval);
        }
        isBursting = false;
    }


    void TriggerMuzzleFlash()
    {
        muzzleFlashParticle.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}
