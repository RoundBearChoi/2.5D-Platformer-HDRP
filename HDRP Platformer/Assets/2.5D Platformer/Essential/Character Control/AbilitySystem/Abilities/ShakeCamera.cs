using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/ShakeCamera")]
    public class ShakeCamera : CharacterAbility
    {
        [Range(0f, 0.99f)]
        public float ShakeTiming;
        public float ShakeLength;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (ShakeTiming == 0f)
            {
                CameraManager.Instance.ShakeCamera(ShakeLength);
                characterState.DATASET.CAMERA_DATA.CameraShaken = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.DATASET.CAMERA_DATA.CameraShaken)
            {
                if (stateInfo.normalizedTime >= ShakeTiming)
                {
                    characterState.DATASET.CAMERA_DATA.CameraShaken = true;
                    CameraManager.Instance.ShakeCamera(ShakeLength);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.DATASET.CAMERA_DATA.CameraShaken = false;
        }
    }
}