using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerAnimation : CharacterUpdate
    {
        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            if (control.UpdatingAbility(typeof(LockTransition)))
            {
                if (control.TRANSITION_DATA.LockTransition)
                {
                    control.ANIMATOR.SetBool(
                        HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        true);
                }
                else
                {
                    control.ANIMATOR.SetBool(
                        HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                        false);
                }
            }
            else
            {
                control.ANIMATOR.SetBool(
                    HashManager.Instance.ArrMainParams[(int)MainParameterType.LockTransition],
                    false);
            }
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}