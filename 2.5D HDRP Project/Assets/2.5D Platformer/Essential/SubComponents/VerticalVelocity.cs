using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class VerticalVelocity : SubComponent
    {
        public override void InitComponent()
        {
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.VERTICAL_VELOCITY] = this;
        }

        public override void OnFixedUpdate()
        {
            // jump cancel after letting go
            if (!control.VERTICAL_VELOCITY_DATA.NoJumpCancel)
            {
                if (control.RIGID_BODY.velocity.y > 0f && !control.Jump)
                {
                    control.RIGID_BODY.velocity -= (Vector3.up * control.RIGID_BODY.velocity.y * 0.1f);
                }
            }

            // slow down wallslide
            if (control.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity.y != 0f)
            {
                if (control.RIGID_BODY.velocity.y <= control.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity.y)
                {
                    control.RIGID_BODY.velocity = control.VERTICAL_VELOCITY_DATA.MaxWallSlideVelocity;
                }
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}