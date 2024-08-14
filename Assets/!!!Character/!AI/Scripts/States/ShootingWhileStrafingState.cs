using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWhileStrafingState : ShootingStandingState
{

    protected override void InternalPreUpdate()
    {
        entity.agent.SetDestination(entity.transform.position + entity.transform.right);
    }

}
