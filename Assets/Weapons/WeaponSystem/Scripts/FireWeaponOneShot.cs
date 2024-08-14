
public class FireWeaponOneShot : FireWeapon
{
    public override bool CanUse() { return true; }

    public override bool CanContinuousUse() { return false; }

}
