using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : CharacterUpdate
    {
        LedgeGrabData LGDATA => control.DATASET.LEDGE_GRAB_DATA;

        public override void InitComponent()
        {

        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            if (control.ANIMATOR.GetBool(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                if (control.RIGID_BODY.useGravity)
                {
                    LGDATA.isGrabbingLedge = false;
                }
            }

            if (IsLedgeGrabTriggerState())
            {
                if (LedgeCollidersInPosition() &&
                    control.Jump)
                {
                    if (!LGDATA.isGrabbingLedge)
                    {
                        control.RunFunction(typeof(DoLedgeGrab));
                        LGDATA.isGrabbingLedge = true;
                    }
                }
                else
                {
                    LGDATA.isGrabbingLedge = false;
                }
            }
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }

        bool IsLedgeGrabTriggerState()
        {
            if (!control.Jump)
            {
                return false;
            }

            for (int i = 0; i < HashManager.Instance.ArrLedgeTriggerStates.Length; i++)
            {
                AnimatorStateInfo info = control.ANIMATOR.GetCurrentAnimatorStateInfo(0);
                if (info.shortNameHash == HashManager.Instance.ArrLedgeTriggerStates[i])
                {
                    return true;
                }
            }

            return false;
        }

        bool LedgeCollidersInPosition()
        {
            foreach (GameObject obj in LGDATA.collider1.CollidedObjects)
            {
                if (!LGDATA.collider2.CollidedObjects.Contains(obj))
                {
                    LGDATA.TargetLedge = obj;
                    return true;
                }
                else
                {
                    LGDATA.TargetLedge = null;
                    return false;
                }
            }

            LGDATA.TargetLedge = null;
            return false;
        }
    }
}