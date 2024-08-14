using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecissionTargetIsInRange : DecissionNode
{
    EntityDescissionMaker descissionMaker;

    protected override void InternalAwake()
    {
        descissionMaker = GetComponentInParent<EntityDescissionMaker>();
    }

    protected override bool CheckCondition()
    {
        return Vector3.Distance(
            descissionMaker.currentTarget.GetTransform().position, transform.position) < 
            descissionMaker.GetEntityWeapon().GetEffectiveRange();
        
    }

}
