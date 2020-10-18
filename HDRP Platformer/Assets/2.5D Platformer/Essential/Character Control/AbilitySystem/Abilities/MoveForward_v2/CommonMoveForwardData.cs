using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CommonMoveForwardData
    {
        public delegate float GetFloat();
        public delegate bool GetBool();

        public GetFloat GetBlockDistance;
        public GetFloat GetMoveSpeed;
        public GetBool IsMoveOnHit;
    }
}