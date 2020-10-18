using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/MoveForward_v2")]
    public class MoveForward_v2 : CharacterAbility
    {
        CommonMoveForwardData commonForwardData = null;

        [Space(10)]
        [SerializeField] BasicMovementOptions basicMovementOptions;
        [Space(10)]
        [SerializeField] MomentumMovementOptions momentumOptions;
        [Space(10)]
        [SerializeField] HashClassKey[] DoNotMove;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            SetCommonMoveComponent();
            characterState.control.MOVE_DATA.LatestMoveForward = commonForwardData;
            SetStartingMomentum(characterState);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.control.MOVE_DATA.LatestMoveForward != commonForwardData)
            {
                return;
            }

            AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(0);

            for (int i = 0; i < DoNotMove.Length; i++)
            {
                if (nextStateInfo.shortNameHash == DoNotMove[i].ShortNameHash)
                {
                    return;
                }
            }

            ConstantMove(characterState.control, stateInfo);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (momentumOptions.UseMomentum)
            {
                if (momentumOptions.ClearMomentumOnExit)
                {
                    characterState.control.MOVE_DATA.Momentum = 0f;
                }
            }
        }

        void SetCommonMoveComponent()
        {
            if (commonForwardData == null)
            {
                commonForwardData = new CommonMoveForwardData();

                commonForwardData.GetBlockDistance = ReturnBlockDistance;
                commonForwardData.GetMoveSpeed = ReturnMoveSpeed;
                commonForwardData.IsMoveOnHit = ReturnMoveOnHit;
            }
        }

        public float ReturnBlockDistance()
        {
            return basicMovementOptions.BlockDistance;
        }

        public float ReturnMoveSpeed()
        {
            return basicMovementOptions.Speed;
        }

        public bool ReturnMoveOnHit()
        {
            return false;
        }

        void SetStartingMomentum(CharacterState characterState)
        {
            if (momentumOptions.UseMomentum)
            {
                if (!momentumOptions.StartFromPreviousMomentum)
                {
                    if (momentumOptions.StartingMomentum > 0.001f)
                    {
                        if (characterState.control.GetBool(typeof(FacingForward)))
                        {
                            characterState.control.MOVE_DATA.Momentum =
                                momentumOptions.StartingMomentum;
                        }
                        else
                        {
                            characterState.control.MOVE_DATA.Momentum =
                                -momentumOptions.StartingMomentum;
                        }
                    }
                }
            }
        }

        private void ConstantMove(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (!control.GetBool(typeof(FrontIsBlocked)))
            {
                control.RunFunction(typeof(MoveTransformForward),
                    basicMovementOptions.Speed,
                    basicMovementOptions.SpeedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }
    }
}