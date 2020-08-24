using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/MoveForward_v2")]
    public class MoveForward_v2 : CharacterAbility
    {
        MoveForwardComponent moveForwardComponent;
        [Space(10)]
        [SerializeField] BasicMovementOptions basicMovementOptions;
        [Space(10)]
        [SerializeField] MomentumMovementOptions momentumOptions;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //characterState.ANIMATION_DATA.LatestMoveForward = this;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.ANIMATION_DATA.LatestMoveForward != moveForwardComponent)
            {
                return;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}