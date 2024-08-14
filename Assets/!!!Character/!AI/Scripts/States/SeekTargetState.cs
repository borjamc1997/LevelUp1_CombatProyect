using System;
using UnityEngine;
using UnityEngine.AI;

public class SeekTargetState : State
{

    private void Update()
    {
        if (entity.sight.visiblesInSight.Count > 0)
        {
            entity.agent.destination = entity.sight.visiblesInSight[0].GetTransform().position;
        }
    }
}
