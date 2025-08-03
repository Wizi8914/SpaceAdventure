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
    [SerializeField] Transform bulletSpawnLocation;
    [SerializeField] int bulletsPerShot;
    [SerializeField] float bulletVelocity;
    [SerializeField] float bulletHoleSizeMultiplier;
    public float damage = 10;

    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip noAmmoSound;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public WeaponAmmo ammo;
    WeaponBloom bloom;
    Light muzzleFlashLight;
    ParticleSystem muzzleFlashPArticle;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20f;

    public float ennemyKickBackForce = 100f;

    public Transform leftHandTarget, leftHandHint;


    void Start()
    {
        ammo = GetComponent<WeaponAmmo>();
        bloom = GetComponent<WeaponBloom>();
        muzzleFlashLight = bulletSpawnLocation.GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0f;
        muzzleFlashPArticle = bulletSpawnLocation.GetComponentInChildren<ParticleSystem>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
        {
            if (isBurstFire && !isBursting)
            {
                StartCoroutine(BurstFire());
            }
            else
            {
                Fire();
            }
        }

        if (ammo.currentAmmo <= 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            audioSource.PlayOneShot(noAmmoSound);
        }

        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0f, lightReturnSpeed * Time.deltaTime);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo <= 0) return false;
        return false;

    }

    void Fire()
    {
        fireRateTimer = 0f;
        bulletSpawnLocation.LookAt(GameManager.Instance.player.transform.position);
        bulletSpawnLocation.localEulerAngles = bloom.bloomAngle(bulletSpawnLocation);

        // Play fire sound
        audioSource.pitch = Random.Range(0.85f, 1.1f);
        audioSource.volume = Random.Range(0.8f, 1f);
        //audioSource.time = Random.Range(0f, fireSound.length);
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
        muzzleFlashPArticle.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}
