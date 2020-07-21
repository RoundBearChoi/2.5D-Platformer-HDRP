using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Left : CheckConditionBase
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