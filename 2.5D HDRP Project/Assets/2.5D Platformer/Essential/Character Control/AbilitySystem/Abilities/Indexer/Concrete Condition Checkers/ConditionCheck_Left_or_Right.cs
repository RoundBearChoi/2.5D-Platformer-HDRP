using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Left_or_Right : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.MoveLeft && !control.MoveRight)
            {
                return false;
            }

            if (control.MoveLeft && control.MoveRight)
            {
                return false;
            }

            return true;
        }
    }
}