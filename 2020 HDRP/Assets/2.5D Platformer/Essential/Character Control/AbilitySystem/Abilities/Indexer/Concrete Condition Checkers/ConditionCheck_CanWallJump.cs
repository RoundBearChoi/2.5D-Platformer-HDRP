using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_CanWallJump : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.JUMP_DATA.CanWallJump)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}