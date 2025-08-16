using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState currentState;

    public DefaultState Default = new DefaultState();
    public ReloadState Reload = new ReloadState();
    public SwapState Swap = new SwapState();

    [HideInInspector] public PlayerInput playerInput;

    [HideInInspector] public WeaponManager currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;
    AudioSource audioSource;

    [HideInInspector] public Animator animator;
    public MultiAimConstraint rHandAnim;
    public TwoBoneIKConstraint lHandIK;

    void Start()
    {
        SwitchState(Default);
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        ammo.Reload();
        rHandAnim.weight = 0.3f;
        lHandIK.weight = 1f;
        SwitchState(Default);
    }

    public void MagazineIn() => audioSource.PlayOneShot(ammo.magazineOutSound);

    public void MagazineOut() => audioSource.PlayOneShot(ammo.magazineInSound);

    public void ReleaseSlide() => audioSource.PlayOneShot(ammo.releaseSlideSource);

    public void SetWeapon(WeaponManager weapon)
    {
        currentWeapon = weapon;
        audioSource = weapon.audioSource;
        ammo = weapon.ammo;
    }
    
}
