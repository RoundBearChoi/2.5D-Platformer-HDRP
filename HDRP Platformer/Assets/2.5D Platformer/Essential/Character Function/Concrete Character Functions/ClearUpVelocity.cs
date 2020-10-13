using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ClearUpVelocity : CharacterFunction
    {
        public override void RunFunction()
        {
            control.RIGID_BODY.velocity = new Vector3(
                control.RIGID_BODY.velocity.x,
                0f,
                control.RIGID_BODY.velocity.z);
        }
    }
}