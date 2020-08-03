using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_BlockedByWall_NOT : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!ConditionCheck_BlockedByWall.IsTrue(control))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}