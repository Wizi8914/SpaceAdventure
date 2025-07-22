using UnityEngine;

public class WeaponBox : MonoBehaviour, IInteractable

{
    public string InteractableName => InteractionMessage;

    [SerializeField] string InteractionMessage = "Press [E] to obtain ";
    [SerializeField] Transform weaponHolder;
    [SerializeField] GameObject WeaponPrefab;
    WeaponBoxWeaponDisplay weaponDisplay;
    private bool isTaken = false;

    // find player in list
    WeaponClassManager playerWeaponManager;

    void Start()
    {
        playerWeaponManager = FindFirstObjectByType<WeaponClassManager>();
        InteractionMessage += WeaponPrefab.GetComponent<WeaponManager>().weaponName;
        weaponDisplay = GetComponentInChildren<WeaponBoxWeaponDisplay>();
    }


    public void Interact()
    {
        if (!isTaken)
        {
            playerWeaponManager.AddNewWeapon(WeaponPrefab);
            isTaken = true;
            weaponDisplay.DisableWeaponDisplay();
            InteractionMessage = "";
        }
    }
}
