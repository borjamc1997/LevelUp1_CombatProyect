using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelByRaycasting : Barrel
{

    [SerializeField] float range = Mathf.Infinity;
    [SerializeField] Transform shootPoint;
    [SerializeField] LayerMask shootLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] GameObject shotTracePrefab;
    protected override void InternalShot()
    {
        ShotTrace shotTrace = null;
        if (shotTracePrefab)
        {
            GameObject shotTraceGO = Instantiate(shotTracePrefab);
            shotTrace = shotTraceGO.GetComponent<ShotTrace>();
        }
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hit, range, shootLayerMask))
        {
            hit.collider.GetComponent<HurtBox>()?.NotifyHit();
            shotTrace?.Init(shootPoint.position, shootPoint.position + shootPoint.forward * hit.distance);
        }
        else
        {
            shotTrace?.Init(shootPoint.position, shootPoint.position + shootPoint.forward * range);
        }
    }
    protected override void InternalStartShooting()
    {

    }
    protected override void InternalStopShooting()
    {

    }

}
