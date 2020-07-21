using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_GrabbingLedge : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.LEDGE_GRAB_DATA.isGrabbingLedge)
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