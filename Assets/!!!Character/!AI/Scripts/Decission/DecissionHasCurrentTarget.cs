using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecissionHasCurrentTarget : DecissionNode
{

    EntityDescissionMaker entityDescissionMaker;

    protected override void InternalAwake()
    {
        entityDescissionMaker = GetComponentInParent<EntityDescissionMaker>();
    }

    protected override bool CheckCondition()
    {
        return entityDescissionMaker.currentTarget != null;
    }

}
