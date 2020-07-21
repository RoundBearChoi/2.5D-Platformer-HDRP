using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Left : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.MoveLeft)
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