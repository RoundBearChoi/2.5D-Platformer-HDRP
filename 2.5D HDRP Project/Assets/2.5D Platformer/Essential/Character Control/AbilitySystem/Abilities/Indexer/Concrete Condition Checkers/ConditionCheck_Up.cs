using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Up : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.MoveUp)
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