using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Right : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.MoveRight)
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