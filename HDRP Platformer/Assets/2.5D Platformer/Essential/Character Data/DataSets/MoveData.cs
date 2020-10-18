namespace Roundbeargames
{
    [System.Serializable]
    public class MoveData
    {
        public CommonMoveForwardData LatestMoveForward = null;
        public MoveUp LatestMoveUp = null;
        public bool IsIgnoreCharacterTime = false;
        public float Momentum = 0f;
    }
}