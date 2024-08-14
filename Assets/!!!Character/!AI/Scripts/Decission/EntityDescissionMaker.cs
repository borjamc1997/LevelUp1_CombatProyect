using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class EntityDescissionMaker : MonoBehaviour
{

    [SerializeField] Transform enemy;

    private State[] allStates;

    [Header("Senses")]
    private Sight sight;

    private EntityWeapons entityWeapons;

    public IVisible currentTarget { get; private set; }

    private DecissionTreeNode decissionRoot;


    private void Awake()
    {

        allStates = enemy.GetComponents<State>();
        entityWeapons = enemy.GetComponent<EntityWeapons>();
        sight = enemy.GetComponent<Sight>();

        decissionRoot = transform.GetChild(0).GetComponent<DecissionTreeNode>();

        foreach (State s in allStates) { s.enabled = false; }
    }

    private void Update()
    {
        currentTarget = sight.visiblesInSight.Count > 0 ? sight.visiblesInSight[0] : null;

        SetState(decissionRoot.Execute());
    }

    internal void SetState(State newState)
    {
        foreach (State state in allStates)
        {
            bool mustBeEnable = state == newState;
            if (state.enabled != mustBeEnable) 
            { state.enabled = mustBeEnable; }
        }
    }

    internal EntityWeapons GetEntityWeapon()
    {
        return entityWeapons;
    }
}
