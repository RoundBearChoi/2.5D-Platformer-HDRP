using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/SmoothTurn")]
    public class SmoothTurn : CharacterAbility
    {
        public float Speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.TURN_DATA.StartedForward = characterState.characterControl.GetBool(typeof(FacingForward));
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.TURN_DATA.StartedForward)
            {

            }
            else
            {

            }
        }
    }
}