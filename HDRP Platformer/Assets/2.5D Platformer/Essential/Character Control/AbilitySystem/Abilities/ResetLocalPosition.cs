﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/ResetLocalPosition")]
    public class ResetLocalPosition : CharacterAbility
    {
        public bool OnStart;
        public bool OnEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnStart)
            {
                characterState.control.characterSetup.SkinnedMeshAnimator.transform.localPosition = Vector3.zero;
                characterState.control.characterSetup.SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                characterState.control.characterSetup.
                    SkinnedMeshAnimator.transform.localPosition = Vector3.zero;

                characterState.control.characterSetup.
                    SkinnedMeshAnimator.transform.localRotation = Quaternion.identity;
            }
        }
    }
}