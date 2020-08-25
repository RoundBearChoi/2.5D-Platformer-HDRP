using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class OffsetOnFoot
    {
        public bool UseOffsetOnFoot;
        [Space(5)]
        [Range(0f, 1f)]
        public float LeftFootForward_Offset;
        [Space(5)]
        [Range(0f, 1f)]
        public float RightFootForward_Offset;
    }
}