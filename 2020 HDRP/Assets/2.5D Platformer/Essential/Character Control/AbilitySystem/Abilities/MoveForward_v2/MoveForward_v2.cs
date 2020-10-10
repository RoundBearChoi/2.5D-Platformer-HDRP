﻿using System.Collections;
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
        [Space(10)]
        [SerializeField] HashClassKey IdleKey;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (IdleKey == null)
            {
                Debug.LogError("idle key missing: " + this.name);
            }

            SetCommonMoveComponent();
            characterState.ANIMATION_DATA.LatestMoveForward = moveForwardComponent;
            SetStartingMomentum(characterState);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.ANIMATION_DATA.LatestMoveForward != moveForwardComponent)
            {
                return;
            }

            AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(0);

            if (nextStateInfo.shortNameHash == HashManager.Instance.DicHashes[IdleKey])
            {
                return;
            }

            ConstantMove(characterState.characterControl, stateInfo);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (momentumOptions.UseMomentum)
            {
                if (momentumOptions.ClearMomentumOnExit)
                {
                    characterState.MOMENTUM_DATA.Momentum = 0f;
                }
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
                        if (characterState.characterControl.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
                        {
                            characterState.MOMENTUM_DATA.Momentum = momentumOptions.StartingMomentum;
                        }
                        else
                        {
                            characterState.MOMENTUM_DATA.Momentum = -momentumOptions.StartingMomentum;
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

        void SetCommonMoveComponent()
        {
            if (moveForwardComponent == null)
            {
                moveForwardComponent = new MoveForwardComponent();
                moveForwardComponent.GetBlockDistance = ReturnBlockDistance;
                moveForwardComponent.GetMoveSpeed = ReturnMoveSpeed;
                moveForwardComponent.IsMoveOnHit = ReturnMoveOnHit;
            }
        }
    }
}