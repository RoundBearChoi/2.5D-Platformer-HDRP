using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/AnimatorStateTransition")]
    public class AnimatorStateTransition : CharacterAbility
    {
        static bool DebugTransitionTiming = true;

        [SerializeField] TransitionTarget transitionTo;
        int TargetStateNameHash = 0;

        [Space(10)]
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();
        [Space(5)]
        public List<TransitionConditionType> notConditions = new List<TransitionConditionType>();

        [Space(10)]
        [SerializeField] ExitTimeTransition exitTimeTransition;

        [Space(10)]
        public float CrossFade;
        public float Offset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            TargetStateNameHash = transitionTo.GetHashID();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!Interfered(characterState.characterControl))
            {
                if (!exitTimeTransition.UseExitTime)
                {
                    if (IndexChecker.MakeTransition(characterState.characterControl, transitionConditions))
                    {
                        if (!IndexChecker.NotCondition(characterState.characterControl, notConditions))
                        {
                            characterState.ANIMATION_DATA.InstantTransitionMade = true;
                            MakeInstantTransition(characterState.characterControl);
                        }
                    }
                }
                else
                {
                    if (exitTimeTransition.TransitionTime <= stateInfo.normalizedTime)
                    {
                        characterState.ANIMATION_DATA.InstantTransitionMade = true;
                        MakeInstantTransition(characterState.characterControl);
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
            SetMirror(control);

            if (CrossFade <= 0f)
            {
                control.characterSetup.SkinnedMeshAnimator.Play(TargetStateNameHash, 0);
            }
            else
            {
                if (DebugTransitionTiming)
                {
                    //Debug.Log("Instant transition to: " + TransitionTo.ToString() + " - CrossFade: " + CrossFade);
                }

                if (Offset <= 0f)
                {
                    control.characterSetup.SkinnedMeshAnimator.CrossFade(TargetStateNameHash, CrossFade, 0);
                }
                else
                {
                    control.characterSetup.SkinnedMeshAnimator.CrossFade(TargetStateNameHash, CrossFade, 0, Offset);
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

            if (control.characterSetup.SkinnedMeshAnimator.GetInteger(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.TransitionIndex]) != 0)
            {
                return true;
            }

            AnimatorStateInfo nextInfo = control.characterSetup.SkinnedMeshAnimator.GetNextAnimatorStateInfo(0);

            if (nextInfo.shortNameHash == TargetStateNameHash)
            {
                return true;
            }

            return false;
        }

        void SetMirror(CharacterControl control)
        {
            MirrorParameterType mirrorParamType = transitionTo.GetNextMirrorType();

            if (mirrorParamType == MirrorParameterType.idle_mirror)
            {
                if (control.GetBool(typeof(RightFootIsForward)))
                {
                    control.characterSetup.SkinnedMeshAnimator.SetBool(
                        HashManager.Instance.ArrMirrorParameters[(int)mirrorParamType], true);
                }
                else
                {
                    control.characterSetup.SkinnedMeshAnimator.SetBool(
                        HashManager.Instance.ArrMirrorParameters[(int)mirrorParamType], false);
                }
            }
        }
    }
}