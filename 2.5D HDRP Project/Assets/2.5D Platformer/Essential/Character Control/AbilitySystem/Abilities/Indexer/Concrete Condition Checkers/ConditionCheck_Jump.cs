using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Jump : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.Jump)
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