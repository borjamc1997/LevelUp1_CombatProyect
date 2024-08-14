using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterDeath : MonoBehaviour
{
    [SerializeField] bool debugDie;

    private void OnValidate()
    {
        if (debugDie)
        {
            debugDie = false;
            Die();
        }
    }
    public void Die()
    {
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent agent)) { agent.enabled = false; }
        if (TryGetComponent<Collider>(out Collider collider)) { collider.enabled = false; }
        if (TryGetComponent<SeekTargetState>(out SeekTargetState nonPlayer)) { nonPlayer.enabled = false; }
        if (TryGetComponent<PlayerController>(out PlayerController playerController)) { playerController.enabled = false; }

        GetComponentInChildren<CharacterRagdollController>()?.ActivateRagdoll();
        Animator animator = GetComponentInChildren<Animator>();
        if (animator) { animator.enabled = false; }
    }

}
