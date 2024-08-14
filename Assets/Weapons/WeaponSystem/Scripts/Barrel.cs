using System;
using UnityEngine;

public abstract class Barrel : MonoBehaviour
{

    public float cadence;
    public int maxAmmo = 12;
    public int currentAmmo = 12;
    public AmmunitionTypeBase ammoType;

    public float rechargeTime = 2.5f;
    public float remainingRechargeTime = 0;
    public float effectiveRange = 15f;
    double lastShotTime = 0f;

    public void Shot()
    {
        if (IsReady())
        {
            ConsumeAmmo();
            InternalShot();
        }
    }

    protected abstract void InternalShot();

    private void Update()
    {
        if (remainingRechargeTime > 0)
        {
            remainingRechargeTime -= Time.deltaTime;
            if (remainingRechargeTime <= 0f)
            {
                remainingRechargeTime = 0f;
                currentAmmo = maxAmmo;
            }
        }
    }

    public void StartShooting()
    {
        InternalStartShooting();
    }
    public void StopShooting()
    {
        InternalStopShooting();
    }

    protected abstract void InternalStartShooting();
    protected abstract void InternalStopShooting();

    protected void ConsumeAmmo()
    {
        currentAmmo--;
        if (currentAmmo <= 0)
        { remainingRechargeTime = rechargeTime; }
        lastShotTime = Time.time;
    }

    internal bool IsReady()
    {
        return (currentAmmo > 0) && ((Time.time - lastShotTime) > (1f / cadence)) && ((Time.time - remainingRechargeTime) > rechargeTime);
    }
}
