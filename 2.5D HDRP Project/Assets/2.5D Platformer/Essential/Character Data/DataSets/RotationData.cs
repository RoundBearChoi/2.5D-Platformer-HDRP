using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class RotationData
    {
        public bool LockTurn;
        public float UnlockTiming;

        public delegate void DoSomething(bool faceForward);
        public DoSomething FaceForward;
    }
}