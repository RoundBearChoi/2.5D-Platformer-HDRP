using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/StartWalking")]
    public class StartWalking : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.aiProgress.SetRandomFlyingKick();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.control.Attack)
            {
                return;
            }

            //jump
            if (characterState.control.aiProgress.EndSphereIsHigher())
            {
                if (characterState.control.aiProgress.AIDistanceToStartSphere() < 0.08f)
                {
                    characterState.control.MoveRight = false;
                    characterState.control.MoveLeft = false;

                    animator.SetBool(
                        HashManager.Instance.ArrAITransitionParams[(int)AI_Transition.jump_platform], true);
                    return;
                }
            }

            //fall
            if (characterState.control.aiProgress.EndSphereIsLower())
            {
                characterState.control.aiController.WalkStraightToEndSphere();

                animator.SetBool(
                    HashManager.Instance.ArrAITransitionParams[(int)AI_Transition.fall_platform], true);
                return;
            }

            //straight
            if (characterState.control.aiProgress.AIDistanceToStartSphere() > 1.5f)
            {
                characterState.control.Turbo = true;
            }
            else
            {
                characterState.control.Turbo = false;
            }

            characterState.control.aiController.WalkStraightToStartSphere();

            if (characterState.control.aiProgress.AIDistanceToEndSphere() < 1f)
            {
                characterState.control.Turbo = false;
                characterState.control.MoveRight = false;
                characterState.control.MoveLeft = false;
            }

            if (characterState.control.aiProgress.TargetIsOnSamePlatform())
            {
                characterState.control.aiProgress.RepositionDestination();
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(
                HashManager.Instance.ArrAITransitionParams[(int)AI_Transition.jump_platform], false);

            animator.SetBool(
                HashManager.Instance.ArrAITransitionParams[(int)AI_Transition.fall_platform], false);
        }
    }
}