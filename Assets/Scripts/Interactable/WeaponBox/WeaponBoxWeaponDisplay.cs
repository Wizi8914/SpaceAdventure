using UnityEngine;

public class WeaponBoxWeaponDisplay : MonoBehaviour
{
    [SerializeField] Transform weaponHolder;
    [SerializeField] GameObject WeaponMesh;
    private GameObject WeaponMeshInstance;

    [SerializeField] bool weaponMeshEnabled;

    private void OnEnable()
    {
        RefreshDisplay();
    }

    private void Update()
    {
    }

    public void RefreshDisplay()
    {
        if (!weaponMeshEnabled)
        {
            if (WeaponMeshInstance != null)
            {
                DestroyImmediate(WeaponMeshInstance);
                WeaponMeshInstance = null;
            }
            return;
        }

        if (weaponHolder.childCount > 0)
        {
            WeaponMeshInstance = weaponHolder.GetChild(0).gameObject;
        }
    
        if (WeaponMeshInstance.name.Replace("(Clone)", "").Trim() != WeaponMesh.name.Trim())
        {
            DestroyImmediate(WeaponMeshInstance);
            WeaponMeshInstance = null;
            SpawnWeaponMesh();
        }
    }

    public void DisableWeaponDisplay()
    {
        weaponMeshEnabled = false;
        RefreshDisplay();
    }

    private void SpawnWeaponMesh()
    {
        WeaponMeshInstance = Instantiate(WeaponMesh, weaponHolder);
        WeaponMeshInstance.transform.localPosition = Vector3.zero;
        WeaponMeshInstance.transform.localRotation = Quaternion.identity;
        WeaponMeshInstance.transform.localScale = Vector3.one;
    }
}
