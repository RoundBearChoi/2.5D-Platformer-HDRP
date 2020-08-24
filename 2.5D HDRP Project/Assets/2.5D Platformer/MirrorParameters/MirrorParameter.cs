using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum MirrorParameterType
    {
        none,
        idle_mirror,
    }

    public static class MirrorParameter
    {
        static Dictionary<NonMovingStateNames, MirrorParameterType> DicNonMovingStateNameMirror =
            new Dictionary<NonMovingStateNames, MirrorParameterType>();

        public static MirrorParameterType GetMirrorParameter(NonMovingStateNames nonMovingStateType)
        {
            if (nonMovingStateType == NonMovingStateNames.Idle_000)
            {
                return MirrorParameterType.idle_mirror;
            }

            return MirrorParameterType.none;
        }
    }
}