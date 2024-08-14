using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombatMessageForwarder : MonoBehaviour
{
    CharacterCombat characterCombat;

    private void Awake()
    {
        characterCombat = GetComponentInParent<CharacterCombat>();
    }

    void OnAnimationAttack(string animationName)
    {
        characterCombat.OnAnimationAttack(animationName);
    }

}
