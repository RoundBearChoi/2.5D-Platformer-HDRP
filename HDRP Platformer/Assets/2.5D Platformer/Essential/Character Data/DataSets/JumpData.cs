using System.Collections.Generic;

namespace Roundbeargames
{
    [System.Serializable]
    public class JumpData
    {
        public Dictionary<int, bool> DicJumped;
        public bool CanWallJump;
        public bool CheckWallBlock;
    }
}