using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShot : MonoBehaviour
{
    FireWeapon weapon;
    private void Awake()
    {
        weapon = GetComponent<FireWeapon>();
    }

    private void Start()
    {
        weapon.Use();
    }

}
