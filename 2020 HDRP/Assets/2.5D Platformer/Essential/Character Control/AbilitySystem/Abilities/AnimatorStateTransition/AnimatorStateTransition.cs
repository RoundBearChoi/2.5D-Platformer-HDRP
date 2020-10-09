using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/AnimatorStateTransition")]
    public class AnimatorStateTransition : CharacterAbility
    {
        [SerializeField] HashClassKey transitionKey;
        [SerializeField] TransitionTarget transitionTo;
        int TargetStateNameHash = 0;

        [Space(10)]
        [Header("All conditions must be met")]
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();
        [Space(5)]
        public List<TransitionConditionType> notConditions = new List<TransitionConditionType>();

        [Space(10)]
        [SerializeField] ExitTimeTransition exitTimeTransition;

        [Space(10)]
        [SerializeField] OffsetOnFoot offsetOnFoot;

        [Space(10)]
        public float CrossFade;
        public float Offset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            TargetStateNameHash = transitionTo.GetHashID();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!PreConditionsNotMet(characterState.characterControl) &&
                !NextAnimatorStateIsDecided(characterState.characterControl) &&
                !BelowExitTimeRequirement(stateInfo) &&
                !ConditionsNotMet(characterState.characterControl))
            {
                if (transitionKey != null)
                {
                    int key = HashManager.Instance.DicHashes[transitionKey];
                    Debug.Log("transition key: " + key);
                    Debug.Log("transition to: " + TargetStateNameHash + " / " + transitionKey.name);
                }

                MakeInstantTransition(characterState.characterControl);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        void MakeInstantTransition(CharacterControl control)
        {
            MirrorSetter.SetMirrorParameter(control, transitionTo);

            if (offsetOnFoot.UseOffsetOnFoot)
            {
                if (control.GetBool(typeof(RightFootIsForward)))
                {
                    Offset = offsetOnFoot.RightFootForward_Offset;
                }
                else
                {
                    Offset = offsetOnFoot.LeftFootForward_Offset;
                }
            }

            if (CrossFade <= 0f)
            {
                control.characterSetup.SkinnedMeshAnimator.Play(TargetStateNameHash, 0);
            }
            else
            {
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

        bool PreConditionsNotMet(CharacterControl control)
        {
            if (control.ANIMATION_DATA.LockTransition)
            {
                return true;
            }

            return false;
        }

        bool NextAnimatorStateIsDecided(CharacterControl control)
        {
            AnimatorStateInfo nextInfo = control.characterSetup.SkinnedMeshAnimator.GetNextAnimatorStateInfo(0);

            if (nextInfo.shortNameHash != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool BelowExitTimeRequirement(AnimatorStateInfo stateInfo)
        {
            if (!exitTimeTransition.UseExitTime)
            {
                return false;
            }

            if (stateInfo.normalizedTime < exitTimeTransition.TransitionTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool ConditionsNotMet(CharacterControl control)
        {
            if (IndexChecker.MakeTransition(control, transitionConditions))
            {
                if (!IndexChecker.NotCondition(control, notConditions))
                {
                    return false;
                }
            }

            return true;
        }
    }
}