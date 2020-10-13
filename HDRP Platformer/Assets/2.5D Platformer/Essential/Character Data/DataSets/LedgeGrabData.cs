using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class LedgeGrabData
    {
        public bool isGrabbingLedge;
        public LedgeCollider collider1;
        public LedgeCollider collider2;
        public GameObject TargetLedge;
    }
}