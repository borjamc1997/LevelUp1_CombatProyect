using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    IMovementReadable movementReadable;
    IAttackReadable attackReadable;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movementReadable = GetComponent<IMovementReadable>();
        attackReadable = GetComponent<IAttackReadable>();
    }

    Vector3 currentLocalCharacterVelocity = Vector3.zero;
    [SerializeField] private float movementSmoothingSpeed = 1f;

    private void Update()
    {
        UpdatePlaneMovementAnimation();
        UpdateVerticalMovementAnimation();
        if (attackReadable != null)
            UpdateAttackAnimation();
    }

    private void UpdateVerticalMovementAnimation()
    {
        Vector3 characterVelocity = movementReadable.GetVelocity();
        float jumpProgress = Mathf.InverseLerp(
            movementReadable.GetJumpSpeed(),
            -movementReadable.GetJumpSpeed(),
            characterVelocity.y);
        animator.SetBool("isGrounded", movementReadable.GetGrounded());
        animator.SetFloat("jumpProgress", jumpProgress);
    }

    private void UpdatePlaneMovementAnimation()
    {
        Vector3 characterVelocity = movementReadable.GetVelocity();
        Vector3 localCharacterVelocity = transform.InverseTransformDirection(characterVelocity);

        Vector3 desiredLocalChararcterVelocity = Vector3.zero;
        desiredLocalChararcterVelocity.x = NormalizeSpeed(localCharacterVelocity.x);
        desiredLocalChararcterVelocity.z = NormalizeSpeed(localCharacterVelocity.z);

        SmootAnimation(desiredLocalChararcterVelocity);

        animator.SetFloat("SidewardSpeed", currentLocalCharacterVelocity.x);
        animator.SetFloat("ForwardSpeed", currentLocalCharacterVelocity.z);
    }

    private void UpdateAttackAnimation()
    {
        if (attackReadable.MustAttack())
        {
            animator.SetTrigger("Attack");
        }
    }

    private void SmootAnimation(Vector3 desiredLocalChararcterVelocity)
    {
        float distance = (desiredLocalChararcterVelocity - currentLocalCharacterVelocity).magnitude;
        float distanceThatWouldNormallyBeApplied = movementSmoothingSpeed * Time.deltaTime;

        currentLocalCharacterVelocity +=
            (desiredLocalChararcterVelocity - currentLocalCharacterVelocity).normalized * Mathf.Min(distanceThatWouldNormallyBeApplied, distance);
    }

    private float NormalizeSpeed(float axisToNormalize)
    {
        if (axisToNormalize < -movementReadable.GetWalkSpeed())
        {
            axisToNormalize = -Mathf.InverseLerp(-movementReadable.GetWalkSpeed(), -movementReadable.GetRunSpeed(), axisToNormalize);
            axisToNormalize -= 1f;
        }
        else if (axisToNormalize > movementReadable.GetWalkSpeed())
        {
            axisToNormalize = Mathf.InverseLerp(movementReadable.GetWalkSpeed(), movementReadable.GetRunSpeed(), axisToNormalize);
            axisToNormalize += 1f;
        }
        else
        {
            axisToNormalize = Mathf.InverseLerp(-movementReadable.GetWalkSpeed(), movementReadable.GetWalkSpeed(), axisToNormalize);
            axisToNormalize *= 2f;
            axisToNormalize -= 1f;
        }
        return axisToNormalize;
    }

}


