using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/UseRootMotion")]
    public class UseRootMotion : CharacterAbility
    {
        public float from;
        public float to;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= from &&
                stateInfo.normalizedTime <= to)
            {
                characterState.characterControl.characterSetup.SkinnedMeshAnimator.applyRootMotion = true;
            }
            else
            {
                characterState.characterControl.characterSetup.SkinnedMeshAnimator.applyRootMotion = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.characterControl.characterSetup.SkinnedMeshAnimator.applyRootMotion = false;
        }
    }
}