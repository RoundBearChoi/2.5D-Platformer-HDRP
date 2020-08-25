using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum MirrorParameterType
    {
        none,

        idle_mirror,
        walk_mirror,
    
        idlepivot_mirror,
    }

    public static class MirrorParameter
    {
        public static MirrorParameterType GetMirrorParameter(NonMovingStateNames nonMovingStateType)
        {
            if (nonMovingStateType == NonMovingStateNames.Idle_000)
            {
                return MirrorParameterType.idle_mirror;
            }

            if (nonMovingStateType == NonMovingStateNames.Idle_Pivot_R180_InPlace)
            {
                return MirrorParameterType.idlepivot_mirror;
            }

            return MirrorParameterType.none;
        }

        public static MirrorParameterType GetMirrorParameter(WalkStateNames walkStateType)
        {
            if (walkStateType == WalkStateNames.Walk_Fwd_InPlace)
            {
                return MirrorParameterType.walk_mirror;
            }

            return MirrorParameterType.none;
        }
    }
}