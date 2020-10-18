using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/InstantTransition")]
    public class InstantTransition : CharacterAbility
    {
        static bool DebugTransitionTiming = true;

        public Instant_Transition_States TransitionTo;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();
        public List<TransitionConditionType> notConditions = new List<TransitionConditionType>();
        public float CrossFade;
        public float Offset;
        public bool IgnoreAttackAbility;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!Interfered(characterState.control))
            {
                if (IndexChecker.MakeTransition(characterState.control, transitionConditions))
                {
                    if (!IndexChecker.NotCondition(characterState.control, notConditions))
                    {
                        characterState.ANIMATION_DATA.InstantTransitionMade = true;
                        MakeInstantTransition(characterState.control);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.ANIMATION_DATA.InstantTransitionMade = false;
        }

        void MakeInstantTransition(CharacterControl control)
        {
            if (CrossFade <= 0f)
            {
                control.ANIMATOR.Play(
                    HashManager.Instance.ArrInstantTransitionStates[(int)TransitionTo],
                    0);
            }
            else
            {
                if (DebugTransitionTiming)
                {
                    Debug.Log("Instant transition to: " + TransitionTo.ToString() + " - CrossFade: " + CrossFade);
                }
                
                if (Offset <= 0f)
                {
                    control.ANIMATOR.CrossFade(
                        HashManager.Instance.ArrInstantTransitionStates[(int)TransitionTo],
                        CrossFade,
                        0);
                }
                else
                {
                    control.ANIMATOR.CrossFade(
                        HashManager.Instance.ArrInstantTransitionStates[(int)TransitionTo],
                        CrossFade,
                        0,
                        Offset);
                }
            }
        }

        bool Interfered(CharacterControl control)
        {
            if (control.ANIMATION_DATA.LockTransition)
            {
                return true;
            }

            if (control.ANIMATION_DATA.InstantTransitionMade)
            {
                return true;
            }

            if (control.ANIMATOR.GetInteger(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex]) != 0)
            {
                return true;
            }

            if (!IgnoreAttackAbility)
            {
                if (control.UpdatingAbility(typeof(Attack)))
                {
                    return true;
                }
            }

            AnimatorStateInfo nextInfo = control.ANIMATOR.GetNextAnimatorStateInfo(0);

            if (nextInfo.shortNameHash == HashManager.Instance.ArrInstantTransitionStates[(int)TransitionTo])
            {
                return true;
            }
            
            return false;
        }
    }
}