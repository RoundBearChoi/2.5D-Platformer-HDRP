using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_GrabbingLedge : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.LEDGE_GRAB_DATA.isGrabbingLedge)
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