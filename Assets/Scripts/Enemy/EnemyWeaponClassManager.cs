using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EnemyWeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    private RigBuilder rigBuilder;
    [HideInInspector] public GameObject weaponPrefab;
    [HideInInspector] public EnemyWeaponManager weaponManager;

    private GameObject weaponMeshInstance;
    public Transform weaponHolder;
    void Awake()
    {
        rigBuilder = GetComponent<RigBuilder>();
    }

    void Update()
    {

    }

    private IEnumerator RefreshRig()
    {
        yield return null;
        rigBuilder.Build();
    }

    public void DropWeapon(float despawnTime = 5f)
    {
        if (weaponMeshInstance == null) return;

        weaponMeshInstance.transform.SetParent(null);
        weaponMeshInstance.GetComponent<BoxCollider>().enabled = true;

        weaponMeshInstance.AddComponent<Rigidbody>();
        weaponMeshInstance.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
        Destroy(weaponMeshInstance, despawnTime);
    }

    public void updateLeftHandIK()
    {
        if (weaponMeshInstance == null) return;

        EnemyWeaponManager weapon = weaponMeshInstance.GetComponent<EnemyWeaponManager>();
        leftHandIK.data.target = weapon.leftHandTarget;
        leftHandIK.data.hint = weapon.leftHandHint;

        leftHandIK.weight = 1f;
        leftHandIK.data.targetPositionWeight = 1f;
        leftHandIK.data.targetRotationWeight = 1f;

        StartCoroutine(RefreshRig());
    }


    public void UpdateWeapon()
    {

        weaponMeshInstance = Instantiate(weaponPrefab, weaponHolder);
        weaponManager = weaponMeshInstance.GetComponent<EnemyWeaponManager>();

        weaponMeshInstance.transform.localPosition = weaponPrefab.transform.localPosition;
        weaponMeshInstance.transform.localRotation = weaponPrefab.transform.localRotation;
        weaponMeshInstance.transform.localScale = weaponPrefab.transform.localScale;
        

        updateLeftHandIK();
    }
}