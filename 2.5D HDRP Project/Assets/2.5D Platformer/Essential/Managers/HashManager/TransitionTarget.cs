namespace Roundbeargames
{
    [System.Serializable]
    public class TransitionTarget
    {
        public NonMovingStateNames[] nonMovingState;
        public WalkStateNames[] walkState;
        public RunStateNames[] runState;
        public Combo01StateNames[] combo01State;
        public StandingJumpStateNames[] standingJumpState;

        public int GetHashID()
        {
            if (nonMovingState.Length > 0)
            {
                return HashManager.Instance.ArrNonMovingStates[(int)nonMovingState[0]];
            }

            if (walkState.Length > 0)
            {
                return HashManager.Instance.ArrWalkStates[(int)walkState[0]];
            }

            if (runState.Length > 0)
            {
                return HashManager.Instance.ArrRunStates[(int)runState[0]];
            }

            if (combo01State.Length > 0)
            {
                return HashManager.Instance.ArrCombo01States[(int)combo01State[0]];
            }

            if (standingJumpState.Length > 0)
            {
                return HashManager.Instance.ArrStandingJumpStates[(int)standingJumpState[0]];
            }

            return 0;
        }

        public MirrorParameterType GetNextMirrorType()
        {
            if (nonMovingState.Length > 0)
            {
                return MirrorParameter.GetMirrorParameter(nonMovingState[0]);
            }

            if (walkState.Length > 0)
            {
                return MirrorParameter.GetMirrorParameter(walkState[0]);
            }

            if (runState.Length > 0)
            {
                return MirrorParameter.GetMirrorParameter(runState[0]);
            }

            if (combo01State.Length > 0)
            {
                return MirrorParameter.GetMirrorParameter(combo01State[0]);
            }

            if (standingJumpState.Length > 0)
            {
                return MirrorParameter.GetMirrorParameter(standingJumpState[0]);
            }

            return MirrorParameterType.none;
        }
    }
}