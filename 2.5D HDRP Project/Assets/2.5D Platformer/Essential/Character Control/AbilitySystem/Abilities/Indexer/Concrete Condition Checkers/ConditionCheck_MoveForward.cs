using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_MoveForward : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.ROTATION_DATA.IsFacingForward())
            {
                if (!control.MoveRight)
                {
                    return false;
                }
            }
            else
            {
                if (!control.MoveLeft)
                {
                    return false;
                }
            }

            return true;
        }
    }
}