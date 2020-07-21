using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Up : CheckConditionBase
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