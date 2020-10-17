using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/AIAttack")]
    public class AIAttack : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.Turbo = false;
            characterState.control.Attack = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.control.aiProgress.TargetIsDead())
            {
                characterState.control.aiProgress.DoAttack();
            }
            else
            {
                characterState.control.MoveRight = false;
                characterState.control.MoveLeft = false;
                characterState.control.Attack = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
