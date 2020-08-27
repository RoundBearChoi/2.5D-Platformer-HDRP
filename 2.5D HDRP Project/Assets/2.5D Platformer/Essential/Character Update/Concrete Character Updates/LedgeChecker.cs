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
                        DoLedgeGrab(TargetLedge);
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

        bool DoLedgeGrab(GameObject platform)
        {
            BoxCollider boxCollider = platform.GetComponent<BoxCollider>();

            if (boxCollider == null)
            {
                return false;
            }

            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;

            float y, z;
            y = platform.transform.position.y + (boxCollider.size.y / 2f);
            if (control.GetBool(typeof(FacingForward)))
            {
                z = platform.transform.position.z - (boxCollider.size.z / 2f);
            }
            else
            {
                z = platform.transform.position.z + (boxCollider.size.z / 2f);
            }

            Vector3 platformEdge = new Vector3(0f, y, z);
            Vector3 ledgeCalibration = control.characterSetup.ledgeSetup.LedgeCalibration;

            if (control.GetBool(typeof(FacingForward)))
            {
                control.RIGID_BODY.MovePosition(
                    platformEdge + ledgeCalibration);
            }
            else
            {
                control.RIGID_BODY.MovePosition(
                    platformEdge + new Vector3(0f, ledgeCalibration.y, -ledgeCalibration.z));
            }

            return true;
        }
    }
}