using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class CollidingObjData
    {
        public Dictionary<TriggerDetector, List<Collider>> CollidingWeapons;
        public Dictionary<TriggerDetector, List<Collider>> CollidingBodyParts;
    }
}