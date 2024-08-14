using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWeapons : MonoBehaviour
{

    private Weapon[] weapons;
    [SerializeField] private Transform weaponParent;
    private int currentWeaponIndex = 0;
    private bool mustShoot = false;



    private void Awake()
    {
        weapons = weaponParent.GetComponentsInChildren<Weapon>();
    }

    private bool oldMustShoot = false;
    private void Update()
    {
        Weapon currentWeapon = weapons[currentWeaponIndex];
        if (currentWeapon is MeleeWeaponOneUse)
        {

        }
        else if (currentWeapon is FireWeapon)
        {
            FireWeapon fireWeapon = currentWeapon as FireWeapon;
            if (fireWeapon.CanUse())
            {
                if (mustShoot && fireWeapon.ShotReady())
                {
                    fireWeapon.Use();
                }
            }
            else if (fireWeapon.CanContinuousUse()) 
            {
                if (oldMustShoot != mustShoot)
                {
                    if (mustShoot) { fireWeapon.StartUsing(); }
                    else { fireWeapon.StopUsing(); }
                }
            }
        }
        oldMustShoot = mustShoot;
    }

    public void MustShoot() { mustShoot = true; }
    public void MustShootNot() { mustShoot = false; }

    internal float GetEffectiveRange()
    {
        float effectiveRange = 0f;
        Weapon currentWeapon = weapons[currentWeaponIndex];
        if (currentWeapon)
        {
            effectiveRange = currentWeapon.GetEffectiveRange();
        }
        return effectiveRange;
    }
}
