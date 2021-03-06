﻿using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class RagdollData
    {
        public bool RagdollTriggered = false;
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