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

            }

            return MirrorParameterType.none;
        }

        public static MirrorParameterType GetMirrorParameter(RunStateNames runStateType)
        {
            if (runStateType == RunStateNames.Run_Fwd_Start_InPlace)
            {
                return MirrorParameterType.runstart_mirror;
            }

            if (runStateType == RunStateNames.Run_Stop_InPlace)
            {
                return MirrorParameterType.runstop_mirror;
            }

            return MirrorParameterType.none;
        }

        public static MirrorParameterType GetMirrorParameter(Combo01StateNames combo01StateType)
        {
            if (combo01StateType == Combo01StateNames.Frank_RPG_Fighter_Combo01_2)
            {

            }

            return MirrorParameterType.none;
        }

        public static MirrorParameterType GetMirrorParameter(StandingJumpStateNames standingJumpStateType)
        {
            if (standingJumpStateType == StandingJumpStateNames.Jump_3m_sumo_prep ||
                standingJumpStateType == StandingJumpStateNames.Jump_3m_sumo_air ||
                standingJumpStateType == StandingJumpStateNames.Jump_3m_sumo_land)
            {
                return MirrorParameterType.standingjump_mirror;
            }

            return MirrorParameterType.none;
        }
    }
}