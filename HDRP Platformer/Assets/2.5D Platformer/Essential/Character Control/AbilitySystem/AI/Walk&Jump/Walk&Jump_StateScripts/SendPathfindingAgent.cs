using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/SendPathfindingAgent")]
    public class SendPathfindingAgent : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.control.aiProgress.pathfindingAgent == null)
            {
                GameObject p = Instantiate(Resources.Load("PathfindingAgent", typeof(GameObject)) as GameObject);
                characterState.control.aiProgress.pathfindingAgent = p.GetComponent<PathFindingAgent>();
            }

            characterState.control.aiProgress.pathfindingAgent.owner = characterState.control;
            characterState.control.aiProgress.pathfindingAgent.GetComponent<NavMeshAgent>().enabled = false;

            characterState.control.aiProgress.pathfindingAgent.transform.position =
                characterState.control.transform.position + (Vector3.up * 0.5f);

            characterState.control.navMeshObstacle.carving = false;
            characterState.control.aiProgress.pathfindingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.control.aiProgress.pathfindingAgent.StartWalk)
            {
                animator.SetBool(HashManager.Instance.ArrAITransitionParams[(int)AI_Transition.start_walking], true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(HashManager.Instance.ArrAITransitionParams[(int)AI_Transition.start_walking], false);
        }
    }
}