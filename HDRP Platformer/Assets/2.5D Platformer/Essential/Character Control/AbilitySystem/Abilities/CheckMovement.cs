using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/CheckMovement")]
    public class CheckMovement : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CheckLeftRightUpDown(characterState.control);

            if (characterState.control.MoveLeft || characterState.control.MoveRight)
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Move], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Move], false);
            }

            if (characterState.control.Turbo)
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Turbo], false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        void CheckLeftRightUpDown(CharacterControl control)
        {
            if (control.MoveLeft)
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Left], true);
            }
            else
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Left], false);
            }

            if (control.MoveRight)
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Right], true);
            }
            else
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Right], false);
            }

            if (control.MoveUp)
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Up], true);
            }
            else
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Up], false);
            }

            if (control.MoveDown)
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Down], true);
            }
            else
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Down], false);
            }
        }
    }
}