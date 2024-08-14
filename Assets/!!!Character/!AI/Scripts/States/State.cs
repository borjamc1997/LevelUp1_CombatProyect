using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected Entity entity;

    private void Awake()
    {
        entity = GetComponent<Entity>(); 
        StateAwake();
    }

    private void OnEnable()
    {
        StateOnEnable();
    }

    private void OnDisable()
    {
        StateOnDisable();
    }

    protected virtual void StateOnEnable(){ }

    protected virtual void StateOnDisable(){ }

    protected virtual void StateAwake() { }

}
