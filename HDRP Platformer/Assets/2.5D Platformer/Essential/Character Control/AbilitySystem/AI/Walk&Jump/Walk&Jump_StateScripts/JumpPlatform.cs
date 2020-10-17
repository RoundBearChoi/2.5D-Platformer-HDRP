using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/JumpPlatform")]
    public class JumpPlatform : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.Jump = true;
            characterState.control.MoveUp = true;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.control.Attack)
            {
                return;
            }

            float platformDist = characterState.control.aiProgress.pathfindingAgent.EndSphere.transform.position.y -
                characterState.COLLISION_SPHERE_DATA.FrontSpheres[0].transform.position.y;

            if (platformDist > 0.5f)
            {
                if (characterState.control.aiProgress.pathfindingAgent.StartSphere.transform.position.z <
                characterState.control.aiProgress.pathfindingAgent.EndSphere.transform.position.z)
                {
                    characterState.control.MoveRight = true;
                    characterState.control.MoveLeft = false;
                }
                else
                {
                    characterState.control.MoveRight = false;
                    characterState.control.MoveLeft = true;
                }
            }

            if (platformDist < 0.5f)
            {
                characterState.control.MoveRight = false;
                characterState.control.MoveLeft = false;
                characterState.control.MoveUp = false;
                characterState.control.Jump = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}