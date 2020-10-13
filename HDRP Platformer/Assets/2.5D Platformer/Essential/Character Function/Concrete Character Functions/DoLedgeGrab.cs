using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DoLedgeGrab : CharacterFunction
    {
        public override void RunFunction()
        {
            if (control.LEDGE_GRAB_DATA.TargetLedge != null)
            {
                BoxCollider boxCollider = control.LEDGE_GRAB_DATA.TargetLedge.GetComponent<BoxCollider>();

                if (boxCollider != null)
                {
                    _doLedgeGrab(control.LEDGE_GRAB_DATA.TargetLedge, boxCollider);
                }
            }
        }

        void _doLedgeGrab(GameObject targetLedge, BoxCollider boxCollider)
        {
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;

            float y, z;
            y = targetLedge.transform.position.y + (boxCollider.size.y / 2f);
            if (control.GetBool(typeof(FacingForward)))
            {
                z = targetLedge.transform.position.z - (boxCollider.size.z / 2f);
            }
            else
            {
                z = targetLedge.transform.position.z + (boxCollider.size.z / 2f);
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
        }
    }
}