using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Down : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.MoveDown)
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