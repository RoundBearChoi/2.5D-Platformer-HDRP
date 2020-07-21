using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerAnimation : CharacterUpdate
    {
        public override void InitComponent()
        {
            control.ANIMATION_DATA.IsRunning = IsRunning;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            if (IsRunning(typeof(LockTransition)))
            {
                if (control.ANIMATION_DATA.LockTransition)
                {
                    control.characterSetup.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        true);
                }
                else
                {
                    control.characterSetup.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        false);
                }
            }
            else
            {
                control.characterSetup.SkinnedMeshAnimator.
                    SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                    false);
            }
        }

        bool IsRunning(System.Type type)
        {
            foreach (KeyValuePair<CharacterAbility, int> data in control.ANIMATION_DATA.CurrentRunningAbilities)
            {
                if (data.Key.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }
    }
}