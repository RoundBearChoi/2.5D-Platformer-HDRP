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
            characterState.DATASET.TURN_DATA.StartedForward =
                characterState.control.GetBool(typeof(FacingForward));
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.DATASET.TURN_DATA.StartedForward)
            {
                MakeTurn(characterState.control, -180f);
            }
            else
            {
                MakeTurn(characterState.control, 0f);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.DATASET.TURN_DATA.StartedForward)
            {
                characterState.control.RunFunction(typeof(FaceForward), false);
            }
            else
            {
                characterState.control.RunFunction(typeof(FaceForward), true);
            }
        }

        void MakeTurn(CharacterControl control, float target)
        {
            control.transform.rotation = Quaternion.Lerp(control.transform.rotation, Quaternion.Euler(0f, target, 0f), Speed * Time.deltaTime);
        }
    }
}