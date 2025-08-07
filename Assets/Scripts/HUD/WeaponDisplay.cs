using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    public TMP_Text currentWeaponText;
    public TMP_Text currentAmmoText;
    public TMP_Text ammoLeftText;
    public Image weaponIcon;

    private ActionStateManager actions;

    void Start()
    {
        actions = GameManager.Instance.player.GetComponent<ActionStateManager>();
        updateHUD();
    }

    void Update()
    {
        if (actions.currentWeapon != null)
        {
            updateHUD();
        }
    }

    void updateHUD()
    {
        currentWeaponText.text = actions.currentWeapon.weaponName;
        currentAmmoText.text = actions.currentWeapon.ammo.currentAmmo.ToString();
        ammoLeftText.text = actions.currentWeapon.ammo.extraAmmo.ToString();
        weaponIcon.sprite = actions.currentWeapon.weaponIcon;
    }


}