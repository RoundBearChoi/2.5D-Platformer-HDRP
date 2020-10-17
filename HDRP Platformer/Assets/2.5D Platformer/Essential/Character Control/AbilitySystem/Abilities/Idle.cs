using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/Idle")]
    public class Idle : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Move], false);

            characterState.control.BLOCKING_DATA.FrontBlockingObjs.Clear();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.control.Jump)
            {
                // do nothing
            }
            else
            {
                if (!characterState.UpdatingAbility(typeof(Jump)))
                {
                    characterState.JUMP_DATA.DicJumped.Clear();
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}