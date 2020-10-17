using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/WeaponPickUp")]
    public class WeaponPickUp : CharacterAbility
    {
        public float PickUpTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.WEAPON_DATA.HoldingWeapon =
                characterState.control.GetMeleeWeapon(typeof(GetTouchingMeleeWeapon));
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > PickUpTiming)
            {
                try
                {
                    if (characterState.WEAPON_DATA.HoldingWeapon.control == null)
                    {
                        MeleeWeapon w = characterState.WEAPON_DATA.HoldingWeapon;

                        w.transform.parent = characterState.RIGHT_HAND_ATTACK.transform;
                        w.transform.localPosition = w.CustomPosition;
                        w.transform.localRotation = Quaternion.Euler(w.CustomRotation);

                        w.control = characterState.control;
                        w.triggerDetector.control = characterState.control;

                        w.RemoveWeaponFromDictionary(characterState.control);
                    }
                }
                catch(System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}