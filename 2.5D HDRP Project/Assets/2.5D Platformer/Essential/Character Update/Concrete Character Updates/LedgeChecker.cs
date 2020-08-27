using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : CharacterUpdate
    {
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
                ProcLedgeGrab();
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

        void ProcLedgeGrab()
        {
            if (!control.characterSetup.SkinnedMeshAnimator.GetBool(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                foreach (GameObject obj in control.LEDGE_GRAB_DATA.collider1.CollidedObjects)
                {
                    if (!control.LEDGE_GRAB_DATA.collider2.CollidedObjects.Contains(obj))
                    {
                        if (_ProcLedgeGrab(obj))
                        {
                            break;
                        }
                    }
                    else
                    {
                        control.LEDGE_GRAB_DATA.isGrabbingLedge = false;
                    }
                }
            }
            else
            {
                control.LEDGE_GRAB_DATA.isGrabbingLedge = false;
            }

            if (control.LEDGE_GRAB_DATA.collider1.CollidedObjects.Count == 0)
            {
                control.LEDGE_GRAB_DATA.isGrabbingLedge = false;
            }
        }

        bool _ProcLedgeGrab(GameObject platform)
        {
            BoxCollider boxCollider = platform.GetComponent<BoxCollider>();

            if (boxCollider == null)
            {
                return false;
            }

            if (control.LEDGE_GRAB_DATA.isGrabbingLedge)
            {
                return false;
            }

            control.LEDGE_GRAB_DATA.isGrabbingLedge = true;
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

            if (control.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
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