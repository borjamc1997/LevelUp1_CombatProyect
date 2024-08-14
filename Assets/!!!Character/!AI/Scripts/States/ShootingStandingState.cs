using System;
using UnityEngine;

public class ShootingStandingState : State
{

    EntityWeapons entityWeapons;

    protected override void StateAwake()
    {
        entityWeapons = GetComponent<EntityWeapons>();
    }

    protected override void StateOnEnable()
    {
        entity.agent.destination = transform.position;
        entityWeapons.MustShoot();
    }

    protected override void StateOnDisable()
    {
        entityWeapons.MustShootNot();
    }

    private void Update()
    {
        InternalPreUpdate();
        UpdateAim();
        InternalPostUpdate();
    }

    private void UpdateAim()
    {
        if (entity.sight.visiblesInSight.Count > 0)
        {
            IVisible currentTarget = entity.sight.visiblesInSight[0];
            Vector3 direction = currentTarget.GetTransform().position - transform.position;
            Vector3 directionOnPlane = Vector3.ProjectOnPlane(direction, Vector3.up);
            float angularDistance = Vector3.SignedAngle(transform.forward, directionOnPlane, Vector3.up);

            float angleToApply = entity.angularSpeed * Time.deltaTime;
            angleToApply = Mathf.Min(angleToApply, Mathf.Abs(angularDistance));
            transform.Rotate(new(0f, Mathf.Sign(angularDistance) * angleToApply, 0));
        }
    }

    protected virtual void InternalPreUpdate() { }
    protected virtual void InternalPostUpdate() { }

}
