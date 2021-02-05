using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Pistol,
    MachineGun
}

[Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public int bulletsAvailable;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayer;
}

public class WeaponComponent : MonoBehaviour
{
    public Transform GripLocation => gripIKLocation;
    [SerializeField]
    private Transform gripIKLocation;

    public WeaponStats weaponStats;

    // Initialize
    protected WeaponHolder weaponHolder;
    protected CrosshairScript crosshair;
    protected Camera mainCamera;

    public bool firing { get; private set; }
    public bool reloading { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Initialize(WeaponHolder _weaponHolder, CrosshairScript _crosshair)
    {
        weaponHolder = _weaponHolder;
        crosshair = _crosshair;
    }

    public virtual void StartFiringWeapon()
    {
        firing = true;
        if(weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        firing = false;
        CancelInvoke(nameof(FireWeapon));
    }

    protected virtual void FireWeapon()
    {
        weaponStats.bulletsInClip--;
    }


    public virtual void StartReloading()
    {
        reloading = true;
        ReloadWeapon();
    }

    public virtual void StopReloading()
    {
        reloading = false;
    }

    protected virtual void ReloadWeapon()
    {
        int bulletsToReload = weaponStats.clipSize - weaponStats.bulletsAvailable;

        if(bulletsToReload < 0)
        {
            weaponStats.bulletsInClip = weaponStats.clipSize;
            weaponStats.bulletsAvailable -= weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletsInClip = weaponStats.bulletsAvailable;
            weaponStats.bulletsAvailable = 0;
        }
    }
}
