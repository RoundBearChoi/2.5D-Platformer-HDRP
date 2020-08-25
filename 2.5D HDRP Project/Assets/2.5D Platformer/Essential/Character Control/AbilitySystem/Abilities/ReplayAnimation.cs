using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/ReplayAnimation")]
    public class ReplayAnimation : CharacterAbility
    {
        [Range(0f, 1f)]
        public float ReplayTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= ReplayTiming)
            {
                AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);
                if (nextInfo.shortNameHash == 0)
                {
                    animator.Play(stateInfo.shortNameHash, 0, 0f);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}