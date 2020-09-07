﻿using System.Collections;
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
            if (characterState.characterControl.aiProgress.pathfindingAgent == null)
            {
                GameObject p = Instantiate(Resources.Load("PathfindingAgent", typeof(GameObject)) as GameObject);
                characterState.characterControl.aiProgress.pathfindingAgent = p.GetComponent<PathFindingAgent>();
            }

            characterState.characterControl.aiProgress.pathfindingAgent.owner = characterState.characterControl;
            characterState.characterControl.aiProgress.pathfindingAgent.GetComponent<NavMeshAgent>().enabled = false;

            characterState.characterControl.aiProgress.pathfindingAgent.transform.position =
                characterState.characterControl.transform.position + (Vector3.up * 0.5f);

            characterState.characterControl.navMeshObstacle.carving = false;
            characterState.characterControl.aiProgress.pathfindingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.characterControl.aiProgress.pathfindingAgent.StartWalk)
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