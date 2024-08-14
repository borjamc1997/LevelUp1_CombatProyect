using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecissionNode : DecissionTreeNode
{
    DecissionTreeNode node0;
    DecissionTreeNode node1;

    private void Awake()
    {
        node0 = transform.GetChild(0).GetComponent<DecissionTreeNode>();
        node1 = transform.GetChild(1).GetComponent<DecissionTreeNode>();

        InternalAwake();
    }

    protected virtual void InternalAwake() { }

    internal override State Execute()
    {
        if (CheckCondition())
        {
            return node0.Execute();
        }
        else
        {
            return node1.Execute();
        }
    }

    protected abstract bool CheckCondition();
}
