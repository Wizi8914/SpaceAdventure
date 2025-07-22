using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class WeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint leftHandIK;
    public Transform recoilFollowPos;

    ActionStateManager actions;

    public WeaponManager[] weapon;
    public GameObject weaponHolder;
    int currentWeaponIndex;
    private RigBuilder rigBuilder;

    public GameObject Test;

    private void Awake()
    {
        rigBuilder = GetComponent<RigBuilder>();

        currentWeaponIndex = 0;
        for (int i = 0; i < weapon.Length; i++)
        {
            if (i == 0) weapon[i].gameObject.SetActive(true);
            else weapon[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddNewWeapon(Test);
        }
    } 

    public void SetCurrentWeapon(WeaponManager weapon)
    {
        if (actions == null) actions = GetComponent<ActionStateManager>();

        leftHandIK.data.target = weapon.leftHandTarget;
        leftHandIK.data.hint = weapon.leftHandHint;

        leftHandIK.weight = 1f;
        leftHandIK.data.targetPositionWeight = 1f;
        leftHandIK.data.targetRotationWeight = 1f;

        actions.SetWeapon(weapon);

        StartCoroutine(RefreshRig());
    }

    private IEnumerator RefreshRig()
    {
        yield return null;
        rigBuilder.Build();
    }

    public void ChangeWeapon(float direction)
    {
        weapon[currentWeaponIndex].gameObject.SetActive(false);
        if (direction < 0)
        {
            if (currentWeaponIndex == 0) currentWeaponIndex = weapon.Length - 1;
            else currentWeaponIndex--;
        }
        else
        {
            if (currentWeaponIndex == weapon.Length - 1) currentWeaponIndex = 0;
            else currentWeaponIndex++;
        }
        weapon[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void WeaponPutAway()
    {
        ChangeWeapon(actions.Default.scrollDireciton);
    }

    public void WeaponPulledOut()
    {
        actions.SwitchState(actions.Default);
    }

    public void AddNewWeapon(GameObject newWeapon)
    {
        GameObject weaponInstance = Instantiate(newWeapon);
        weaponInstance.SetActive(false);

        weaponInstance.transform.SetParent(weaponHolder.transform);


        WeaponManager weaponManager = weaponInstance.GetComponent<WeaponManager>();

        weaponInstance.GetComponent<WeaponRecoil>().crosshair = GetComponentInParent<AimStateManager>().crosshair;

        WeaponManager[] newWeapons = new WeaponManager[weapon.Length + 1];
        for (int i = 0; i < weapon.Length; i++)
        {
            newWeapons[i] = weapon[i];
        }
        newWeapons[newWeapons.Length - 1] = weaponManager;
        weapon = newWeapons;

        weaponInstance.transform.localPosition = newWeapon.transform.position;
        weaponInstance.transform.localRotation = newWeapon.transform.rotation;
        weaponInstance.transform.localScale = newWeapon.transform.localScale;


        // Equip the new weapon
        currentWeaponIndex = weapon.Length - 1;
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].gameObject.SetActive(i == currentWeaponIndex);
        }
        SetCurrentWeapon(weapon[currentWeaponIndex]);
    }
}
