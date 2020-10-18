using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/WeaponThrow")]
    public class WeaponThrow : CharacterAbility
    {
        public float ThrowTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > ThrowTiming)
            {
                if (characterState.DATASET.WEAPON_DATA.HoldingWeapon != null)
                {
                    characterState.DATASET.WEAPON_DATA.HoldingWeapon.ThrowWeapon();
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}