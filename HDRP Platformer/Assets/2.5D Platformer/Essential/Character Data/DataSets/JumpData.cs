using System.Collections.Generic;

namespace Roundbeargames
{
    [System.Serializable]
    public class JumpData
    {
        public Dictionary<int, bool> DicJumped = new Dictionary<int, bool>();
        public bool CanWallJump = false;
        public bool CheckWallBlock = false;
    }
}