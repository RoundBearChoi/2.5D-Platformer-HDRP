using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum MirrorParameterType
    {
        none,

        idle_mirror,
        idlepivot_mirror,
        runstart_mirror,
        runstop_mirror,
        standingjump_mirror,
    }

    // animator state name vs mirror parameter

    // Idle_000 -> MirrorParameterType.idle_mirror
    // Idle_Pivot_R180_InPlace -> MirrorParameterType.idlepivot_mirror

    // Run_Fwd_Start_InPlace -> MirrorParameterType.runstart_mirror
    // Run_Stop_InPlace -> MirrorParameterType.runstop_mirror

    // Jump_3m_sumo_prep -> MirrorParameterType.standingjump_mirror
    // Jump_3m_sumo_air -> MirrorParameterType.standingjump_mirror
    // Jump_3m_sumo_land -> MirrorParameterType.standingjump_mirror
}