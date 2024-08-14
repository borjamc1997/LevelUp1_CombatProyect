using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCombat : MonoBehaviour, IAttackReadable
{

    [SerializeField] private bool debugAttack;
    [SerializeField] private Transform hitboxesParents;

    private bool mustAttack = false;
    private bool isCombatWeapon = false;

    private void OnValidate()
    {
        if (debugAttack)
        {
            mustAttack = true;
            debugAttack = false;
        }
    }

    private void Awake()
    {
        GetComponent<PlayerWeapons>().onWeaponSelected.AddListener(OnWeaponSelected);
    }

    private void OnWeaponSelected(Weapon weapon)
    {
        isCombatWeapon = (weapon == null) || !(weapon is FireWeapon); 
    }

    void LateUpdate()
    {
        mustAttack = false;
    }

    private void OnShoot(InputValue analogValue)
    {
        if (isCombatWeapon)
        {
            float newAnalogValue = analogValue.Get<float>();
            if (newAnalogValue > 0f) { mustAttack = true; }
        }
    }

    bool IAttackReadable.MustAttack()
    {
        return mustAttack;
    }

    internal void OnAnimationAttack(string animationName)
    {
        GameObject hitboxGO = hitboxesParents.Find(animationName)?.gameObject;
        if (hitboxGO)
        {
            hitboxGO.SetActive(true);
            DOVirtual.DelayedCall(0.2f, () => hitboxGO.SetActive(false));
        }
    }
}
