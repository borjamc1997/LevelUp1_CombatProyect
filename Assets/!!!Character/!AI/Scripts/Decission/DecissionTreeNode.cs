using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecissionTreeNode : MonoBehaviour
{
    internal abstract State Execute();
}
