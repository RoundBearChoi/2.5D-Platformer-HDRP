using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/TransitionIndexer")]
    public class TransitionIndexer : CharacterAbility
    {
        public int Index;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
            {
                animator.SetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex], Index);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.JUMP_DATA.CheckWallBlock = StartCheckingWallBlock();

            if (animator.GetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex]) == 0)
            {
                if (!characterState.ANIMATION_DATA.LockTransition)
                {
                    if (TransitionConditionChecker.MakeTransition(characterState.characterControl, transitionConditions))
                    {
                        animator.SetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex], Index);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex], 0);
        }

        private bool StartCheckingWallBlock()
        {
            foreach(TransitionConditionType t in transitionConditions)
            {
                if (t == TransitionConditionType.BLOCKED_BY_WALL ||
                    t == TransitionConditionType.NOT_BLOCKED_BY_WALL)
                {
                    return true;
                }
            }

            return false;
        }
    }
}