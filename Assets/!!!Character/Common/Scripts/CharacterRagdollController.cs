using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRagdollController : MonoBehaviour
{
    [SerializeField] private Transform ragdollParent;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private void Awake()
    {
        rigidbodies = ragdollParent.GetComponentsInChildren<Rigidbody>();
        colliders = ragdollParent.GetComponentsInChildren<Collider>();

        DeactivateRagdoll();
    }

    private void DeactivateRagdoll()
    {
        foreach (Rigidbody rigidbody in rigidbodies) { rigidbody.isKinematic = true; }
        foreach (Collider collider in colliders) { collider.enabled = false; }
    }

    public void ActivateRagdoll()
    {
        foreach (Rigidbody rigidbody in rigidbodies) { rigidbody.isKinematic = false; }
        foreach (Collider collider in colliders) { collider.enabled = true; }
    }
}
