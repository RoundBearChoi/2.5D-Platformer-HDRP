using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_MoveForward : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
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