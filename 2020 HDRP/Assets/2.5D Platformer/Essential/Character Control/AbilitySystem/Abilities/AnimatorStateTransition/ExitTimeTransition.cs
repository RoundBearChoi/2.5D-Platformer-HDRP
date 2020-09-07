
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class ExitTimeTransition
    {
        public bool UseExitTime;

        [Range(0f, 1f)]
        public float TransitionTime;
    }
}