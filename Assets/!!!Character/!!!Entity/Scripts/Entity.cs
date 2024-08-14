using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour, IMovementReadable
{
    [SerializeField] State startState;
    State[] states;

    [HideInInspector] public NavMeshAgent agent { get; private set; }
    [HideInInspector] public Sight sight { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] public float angularSpeed { get; private set; } = 360;

    private void Awake()
    {
        states = GetComponents<State>();
        foreach (State state in states)
        {
            state.enabled = state == startState;
        }
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponent<Sight>();

        agent.speed = speed;
    }


    #region IMovementReadable implementation
    Vector3 IMovementReadable.GetVelocity() { return agent.velocity; }
    float IMovementReadable.GetWalkSpeed() { return agent.speed; }
    float IMovementReadable.GetRunSpeed() { return agent.speed; }
    float IMovementReadable.GetJumpSpeed() { return 0f; }
    public bool GetGrounded() { return true; }

    #endregion


}
