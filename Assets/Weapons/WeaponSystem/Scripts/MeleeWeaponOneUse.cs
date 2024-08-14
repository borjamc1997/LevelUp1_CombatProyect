using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponOneUse : Weapon
{
    [SerializeField] float effectiveRange = 1f;
    public override void Use()
    {
        
    }

    public override bool CanUse() { return true; }
    public override bool CanContinuousUse() { return false; }
    public override float GetEffectiveRange() { return effectiveRange; }

}
