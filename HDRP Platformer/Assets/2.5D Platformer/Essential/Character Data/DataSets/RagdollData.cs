using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class RagdollData
    {
        public bool RagdollTriggered;
        public Collider[] ArrBodyParts;
        public FlyingRagdollData flyingRagdollData;
    }

    [System.Serializable]
    public class FlyingRagdollData
    {
        public bool IsTriggered = false;
        public CharacterControl Attacker = null;
    }
}