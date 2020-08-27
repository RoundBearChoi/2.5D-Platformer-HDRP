using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : CharacterUpdate
    {
        GameObject TargetLedge;

        public override void InitComponent()
        {

        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            if (control.characterSetup.SkinnedMeshAnimator.
                GetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                if (control.RIGID_BODY.useGravity)
                {
                    control.LEDGE_GRAB_DATA.isGrabbingLedge = false;
                }
            }

            if (IsLedgeGrabTriggerState())
            {
                if (LedgeCollidersInPosition())
                {
                    if (!control.LEDGE_GRAB_DATA.isGrabbingLedge)
                    {
                        control.RunFunction(typeof(DoLedgeGrab), TargetLedge);
                        control.LEDGE_GRAB_DATA.isGrabbingLedge = true;
                    }
                }
                else
                {
                    control.LEDGE_GRAB_DATA.isGrabbingLedge = false;
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
                AnimatorStateInfo info = control.characterSetup.SkinnedMeshAnimator.GetCurrentAnimatorStateInfo(0);
                if (info.shortNameHash == HashManager.Instance.ArrLedgeTriggerStates[i])
                {
                    return true;
                }
            }

            return false;
        }

        bool LedgeCollidersInPosition()
        {
            foreach (GameObject obj in control.LEDGE_GRAB_DATA.collider1.CollidedObjects)
            {
                if (!control.LEDGE_GRAB_DATA.collider2.CollidedObjects.Contains(obj))
                {
                    TargetLedge = obj;
                    return true;
                }
                else
                {
                    TargetLedge = null;
                    return false;
                }
            }

            TargetLedge = null;
            return false;
        }
    }
}