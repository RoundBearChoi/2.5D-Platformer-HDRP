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
                characterState.control.characterSetup.SkinnedMeshAnimator.applyRootMotion = true;
            }
            else
            {
                characterState.control.characterSetup.SkinnedMeshAnimator.applyRootMotion = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.characterSetup.SkinnedMeshAnimator.applyRootMotion = false;
        }
    }
}