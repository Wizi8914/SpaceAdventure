using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [HideInInspector] public EnemyWeaponManager weapon;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float holeSizeMultiplier;
    [SerializeField] private GameObject bulletImpactPrefab;

    [HideInInspector] public GameObject parentObject;


    ParticleSystem bulletParticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletParticle = GetComponent<ParticleSystem>();
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.transform.root.CompareTag("Player"))
        {
            playerHealth player = collision.gameObject.transform.root.gameObject.GetComponent<playerHealth>();

            player.TakeDamage(weapon.damage, false, parentObject);

            if (player.currentHealth <= 0f && player.isDead == false)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(direction * 10, ForceMode.Impulse);
            }

            SpawnImpactEffect(collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal), false);

            return;
        }
        else
        {
            SpawnImpactEffect(collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal), true);
        }

        bulletParticle.Play();
        Destroy(this.gameObject);
    }

    private void SpawnImpactEffect(Vector3 position, Quaternion rotation, bool isASurfaceImpact = false)
    {
        GameObject impact = Instantiate(bulletImpactPrefab, position, rotation);
        impact.transform.Rotate(0, impact.transform.rotation.x + 180, Random.Range(0, 360));

        DecalProjector decal = impact.GetComponent<DecalProjector>();
        decal.size = new Vector3(decal.size.x * holeSizeMultiplier, decal.size.y * holeSizeMultiplier, decal.size.z * holeSizeMultiplier);

        if (!isASurfaceImpact) decal.enabled = false;
        impact.transform.position -= impact.transform.forward / 100;
    }
}

