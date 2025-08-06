using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EnemyWeaponManager : MonoBehaviour
{   
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;

    [Header("Burst Fire")]
    public bool isBurstFire = false;
    [SerializeField] float burstInterval = 0.08f;
    [HideInInspector] public bool isBursting = false;

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
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticle;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20f;

    public Transform leftHandTarget, leftHandHint;



    void Start()
    {
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

        if (fireRateTimer < fireRate) return false;

        var playerHealth = GameManager.Instance.player.GetComponent<PlayerHealth>();
        if (playerHealth != null && playerHealth.isDead) return false;

        return true;
    }

    public void Fire(Transform target = null)
    {
        fireRateTimer = 0f;

        Vector3 aimTarget = target ? target.position : GameManager.Instance.playerHead.position;
        Vector3 direction = (aimTarget - bulletSpawnLocation.position).normalized;

        Quaternion randomSpread = Quaternion.Euler(
            Random.Range(-inaccuracy, inaccuracy),
            Random.Range(-inaccuracy, inaccuracy),
            0f
        );

        Quaternion spawnRotation = Quaternion.LookRotation(direction) * randomSpread;

        // Play fire sound
        audioSource.pitch = Random.Range(0.85f, 1.1f);
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(fireSound);

        TriggerMuzzleFlash();


        for (int i = 0; i < (isBurstFire ? 1 : bulletsPerShot); i++)
        {
            GameObject currentBullet = Instantiate(bulletPrefab, bulletSpawnLocation.position, spawnRotation);

            EnemyBullet bullet = currentBullet.GetComponent<EnemyBullet>();

            bullet.holeSizeMultiplier = bulletHoleSizeMultiplier;

            bullet.weapon = this; // Assign the weapon to the bullet

            bullet.direction = currentBullet.transform.forward;
            bullet.parentObject = gameObject;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();

            if (rb != null) rb.AddForce(currentBullet.transform.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    public IEnumerator BurstFire()
    {
        isBursting = true;
        for (int i = 0; i < bulletsPerShot; i++)
        {
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
