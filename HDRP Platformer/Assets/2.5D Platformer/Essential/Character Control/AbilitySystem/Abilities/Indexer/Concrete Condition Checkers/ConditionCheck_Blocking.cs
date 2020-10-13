using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Blocking : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.Block)
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