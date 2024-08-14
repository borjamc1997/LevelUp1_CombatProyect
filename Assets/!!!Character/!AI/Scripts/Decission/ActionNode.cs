using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : DecissionTreeNode
{
    [SerializeField] State state;
    EntityDescissionMaker entityDescissionMaker;

    internal override State Execute()
    {
        return state;
    }
}
