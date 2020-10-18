using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/CheckRunningTurn")]
    public class CheckRunningTurn : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.DATASET.ROTATION_DATA.LockTurn)
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], false);
                return;
            }

            if (characterState.control.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
            {
                if (characterState.control.MoveLeft)
                {
                    animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], true);
                }
            }

            if (!characterState.control.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
            {
                if (characterState.control.MoveRight)
                {
                    animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turn], false);
        }
    }
}