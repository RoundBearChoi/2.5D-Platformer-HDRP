using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_MoveBack : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
            {
                if (control.MoveLeft)
                {
                    return true;
                }
            }
            else
            {
                if (control.MoveRight)
                {
                    return true;
                }
            }

            return false;
        }
    }
}