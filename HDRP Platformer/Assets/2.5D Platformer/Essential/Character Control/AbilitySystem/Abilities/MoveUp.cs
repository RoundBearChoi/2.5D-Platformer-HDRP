using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/MoveUp")]
    public class MoveUp : CharacterAbility
    {
        public AnimationCurve SpeedGraph;
        public float Speed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.MOVE_DATA.LatestMoveUp = this;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.control.RIGID_BODY.useGravity)
            {
                if (characterState.control.BLOCKING_DATA.UpBlockingDicCount == 0)
                {
                    characterState.control.transform.
                        Translate(Vector3.up * Speed *
                        SpeedGraph.Evaluate(stateInfo.normalizedTime) *
                        Time.deltaTime);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}