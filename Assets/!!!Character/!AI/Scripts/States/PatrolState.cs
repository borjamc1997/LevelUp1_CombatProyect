using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{

    [SerializeField] private Transform patrolRoute;

    [SerializeField] private float arrivalDistance = 1;
    private int actualPatrolPointIndex = 0;

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        entity.agent.destination = patrolRoute.GetChild(actualPatrolPointIndex).position;
        if (Vector3.SqrMagnitude(entity.agent.destination - transform.position) < arrivalDistance * arrivalDistance)
        {
            actualPatrolPointIndex++;
            if (actualPatrolPointIndex >= patrolRoute.childCount)
            { actualPatrolPointIndex = 0; }
        }
    }
}
