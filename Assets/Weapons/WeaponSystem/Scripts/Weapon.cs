using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] bool debugUse = false;
    [SerializeField] bool debugStartUsing = false;
    [SerializeField] bool debugStopUsing = false;

    private void OnValidate()
    {
        if (debugUse)
        {
            Use();
            debugUse = false;
        }
        if (debugStartUsing)
        {
            StartUsing();
            debugStartUsing = false;
        }
        if (debugStopUsing)
        {
            StopUsing();
            debugStopUsing = false;
        }
    }

    private void Awake()
    {
        InternalAwake();
    }

    protected virtual void InternalAwake() { }

    public virtual void Use() { }
    public virtual void StartUsing() { }
    public virtual void StopUsing() { }

    public abstract bool CanUse();
    public abstract bool CanContinuousUse();
    public abstract float GetEffectiveRange();

    public virtual bool HasGrabPoints() { return false; }
    public virtual Transform GetLeftArmHint() { return null; }
    public virtual Transform GetLeftArmTarget() { return null; }
    public virtual Transform GetRightArmHint() { return null; }
    public virtual Transform GetRightArmTarget() { return null; }
}
