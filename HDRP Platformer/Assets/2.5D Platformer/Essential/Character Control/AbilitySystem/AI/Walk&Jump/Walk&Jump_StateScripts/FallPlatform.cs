using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/FallPlatform")]
    public class FallPlatform : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.control.characterSetup.SkinnedMeshAnimator.GetBool(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                return;
            }

            if (characterState.control.Attack)
            {
                return;
            }

            if (characterState.control.transform.position.z <
                characterState.control.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
            {
                characterState.control.MoveRight = true;
                characterState.control.MoveLeft = false;
            }
            else if (characterState.control.transform.position.z >
                characterState.control.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
            {
                characterState.control.MoveRight = false;
                characterState.control.MoveLeft = true;
            }

            if (characterState.control.aiProgress.AIDistanceToStartSphere() > 3f)
            {
                characterState.control.Turbo = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}