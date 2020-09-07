namespace Roundbeargames
{
    [System.Serializable]
    public class MomentumMovementOptions
    {
        public bool UseMomentum;
        public float StartingMomentum;
        public float MaxMomentum;
        public bool StartFromPreviousMomentum;
        public bool ClearMomentumOnExit;
    }
}