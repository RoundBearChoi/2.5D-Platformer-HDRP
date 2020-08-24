using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class MoveForwardComponent
    {
        public delegate float GetFloat();
        public delegate bool GetBool();

        public GetFloat GetBlockDistance;
        public GetFloat GetMoveSpeed;
        public GetBool IsMoveOnHit;
    }
}