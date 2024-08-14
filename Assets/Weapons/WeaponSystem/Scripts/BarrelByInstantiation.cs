using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelByInstantiation : Barrel
{

    [SerializeField] GameObject prefbToInstantiate;
    [SerializeField] Transform shootPoint;
    [SerializeField] float launchSpeed;
    [SerializeField] float lifeTime = 10;
    protected override void InternalShot()
    {
        GameObject instantatedPrefab = Instantiate(prefbToInstantiate, shootPoint.position, shootPoint.rotation);
        instantatedPrefab.GetComponent<Rigidbody>()?.AddForce(shootPoint.forward * launchSpeed, ForceMode.VelocityChange);
        Destroy(instantatedPrefab, lifeTime);
    }
    protected override void InternalStartShooting()
    {

    }
    protected override void InternalStopShooting()
    {

    }

}
