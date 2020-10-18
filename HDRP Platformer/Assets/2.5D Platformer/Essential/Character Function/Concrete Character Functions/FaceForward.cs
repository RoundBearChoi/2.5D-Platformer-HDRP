using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class FaceForward : CharacterFunction
    {
        public override void RunFunction(bool forward)
        {
            if (control.DisableTurning)
            {
                return;
            }

            if (!control.ANIMATOR.enabled)
            {
                return;
            }

            if (control.ROTATION_DATA.LockTurn)
            {
                return;
            }

            if (forward)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
}