using UnityEngine;

public class BarrelByParticles : Barrel
{
    [SerializeField] private ParticleSystem flameParticleSystem;
    ParticleSystem.EmissionModule emision;

    private void Awake()
    {
        emision = flameParticleSystem.emission;
        emision.enabled = false;
    }
    protected override void InternalShot() { }
    protected override void InternalStartShooting()
    {
        emision.enabled = true;
    }
    protected override void InternalStopShooting()
    {
        emision.enabled = false;
    }

}
