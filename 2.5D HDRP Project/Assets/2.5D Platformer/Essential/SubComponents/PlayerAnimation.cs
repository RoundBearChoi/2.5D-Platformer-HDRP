using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerAnimation : SubComponent
    {
        public override void InitComponent()
        {
            control.ANIMATION_DATA.IsRunning = IsRunning;

            subComponentProcessor.ArrSubComponents[(int)SubComponentType.PLAYER_ANIMATION] = this;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            if (IsRunning(typeof(LockTransition)))
            {
                if (control.animationProgress.LockTransition)
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        true);
                }
                else
                {
                    control.SkinnedMeshAnimator.
                        SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        false);
                }
            }
            else
            {
                control.SkinnedMeshAnimator.
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