using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : CharacterUpdate
    {
        [Header("Ledge Setup")]
        [SerializeField] Vector3 LedgeCalibration = new Vector3();
        [SerializeField] LedgeCollider Collider1;
        [SerializeField] LedgeCollider Collider2;

        public override void InitComponent()
        {
            //temp
            control.LEDGE_GRAB_DATA.Collider1 = Collider1.gameObject;
            control.LEDGE_GRAB_DATA.Collider2 = Collider2.gameObject;

            characterUpdateProcessor.ArrCharacterUpdate[(int)CharacterUpdateType.LEDGECHECKER] = this;
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            if (control.SkinnedMeshAnimator.
                GetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                if (control.RIGID_BODY.useGravity)
                {
                    control.LEDGE_GRAB_DATA.isGrabbingLedge = false;
                }
            }

            if (IsLedgeGrabCondition())
            {
                ProcLedgeGrab();
            }
        }

        bool IsLedgeGrabCondition()
        {
            if (!control.Jump)
            {
                return false;
            }

            for (int i = 0; i < HashManager.Instance.ArrLedgeTriggerStates.Length; i++)
            {
                AnimatorStateInfo info = control.SkinnedMeshAnimator.GetCurrentAnimatorStateInfo(0);
                if (info.shortNameHash == HashManager.Instance.ArrLedgeTriggerStates[i])
                {
                    return true;
                }
            }

            return false;
        }

        void ProcLedgeGrab()
        {
            if (!control.SkinnedMeshAnimator.GetBool(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                foreach (GameObject obj in Collider1.CollidedObjects)
                {
                    if (!Collider2.CollidedObjects.Contains(obj))
                    {
                        if (OffsetPosition(obj))
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

            if (Collider1.CollidedObjects.Count == 0)
            {
                control.LEDGE_GRAB_DATA.isGrabbingLedge = false;
            }
        }

        bool OffsetPosition(GameObject platform)
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
            if (control.ROTATION_DATA.IsFacingForward())
            {
                z = platform.transform.position.z - (boxCollider.size.z / 2f);
            }
            else
            {
                z = platform.transform.position.z + (boxCollider.size.z / 2f);
            }

            Vector3 platformEdge = new Vector3(0f, y, z);

            if (control.ROTATION_DATA.IsFacingForward())
            {
                control.RIGID_BODY.MovePosition(
                    platformEdge + LedgeCalibration);
            }
            else
            {
                control.RIGID_BODY.MovePosition(
                    platformEdge + new Vector3(0f, LedgeCalibration.y, -LedgeCalibration.z));
            }

            return true;
        }
    }
}