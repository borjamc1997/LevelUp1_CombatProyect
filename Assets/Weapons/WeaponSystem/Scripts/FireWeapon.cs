using System;
using UnityEngine;

public abstract class FireWeapon : Weapon
{
    Barrel[] barrels;

    [Header("GrabPoints")]
    [SerializeField] private Transform leftArmHint;
    [SerializeField] private Transform leftArmTarget;
    [SerializeField] private Transform rightArmHint;
    [SerializeField] private Transform rightArmTarget;

    protected override void InternalAwake()
    {
        barrels = GetComponentsInChildren<Barrel>();
    }

    public override void Use()
    {
        foreach(Barrel barrel in barrels)
        {
            barrel.Shot();
        }
    }

    public override void StartUsing()
    {
        foreach(Barrel barrel in barrels)
        {
            barrel.StartShooting();
        }
    }

    public override void StopUsing()
    {
        foreach(Barrel barrel in barrels)
        {
            barrel.StopShooting();
        }
    }

    internal bool ShotReady()
    {
        bool anyBarrelIsReady = false;
        for(int i = 0; i < barrels.Length && !anyBarrelIsReady; i++)
        {
            anyBarrelIsReady = barrels[i].IsReady();
        }
        return anyBarrelIsReady;
    }

    public override float GetEffectiveRange()
    {
        float effectiveRange = 0f;
        foreach (Barrel barrel in barrels)
        {
            effectiveRange = Mathf.Max(effectiveRange, barrel.effectiveRange);
        }
        return effectiveRange;
    }

    public override bool HasGrabPoints() { return true; }
    public override Transform GetLeftArmHint() { return leftArmHint; }
    public override Transform GetLeftArmTarget() { return leftArmTarget; }
    public override Transform GetRightArmHint() { return rightArmHint; }
    public override Transform GetRightArmTarget() { return rightArmTarget; }
}
