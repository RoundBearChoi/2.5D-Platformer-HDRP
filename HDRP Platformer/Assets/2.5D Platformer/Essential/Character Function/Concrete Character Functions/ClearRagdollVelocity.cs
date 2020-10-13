using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ClearRagdollVelocity : CharacterFunction
    {
        public override void RunFunction()
        {
            for (int i = 0; i < control.RAGDOLL_DATA.ArrBodyParts.Length; i++)
            {
                control.RAGDOLL_DATA.ArrBodyParts[i].attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }
}