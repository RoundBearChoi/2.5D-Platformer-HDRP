using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class VerticalVelocity : CharacterUpdate
    {
        VerticalVelocityData VDATA => control.DATASET.VERTICAL_VELOCITY_DATA;

        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            // jump cancel after letting go
            if (!VDATA.NoJumpCancel)
            {
                if (control.RIGID_BODY.velocity.y > 0f && !control.Jump)
                {
                    control.RIGID_BODY.velocity -= (Vector3.up * control.RIGID_BODY.velocity.y * 0.1f);
                }
            }

            // slow down wallslide
            if (VDATA.MaxWallSlideVelocity.y != 0f)
            {
                if (control.RIGID_BODY.velocity.y <= VDATA.MaxWallSlideVelocity.y)
                {
                    control.RIGID_BODY.velocity = VDATA.MaxWallSlideVelocity;
                }
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}