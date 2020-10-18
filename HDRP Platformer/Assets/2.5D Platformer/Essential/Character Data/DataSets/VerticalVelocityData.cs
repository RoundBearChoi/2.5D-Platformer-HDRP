using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class VerticalVelocityData
    {
        public bool NoJumpCancel = false;
        public Vector3 MaxWallSlideVelocity = new Vector3();
    }
}