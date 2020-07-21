using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Blocking_NOT : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.Block)
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