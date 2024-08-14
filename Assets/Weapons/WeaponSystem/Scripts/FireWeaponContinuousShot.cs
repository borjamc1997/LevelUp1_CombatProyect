using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponContinuousShot : FireWeapon
{

    public override bool CanUse() { return false; }

    public override bool CanContinuousUse() { return true; }
}
