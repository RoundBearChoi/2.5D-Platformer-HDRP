using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Turbo_NOT : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.Turbo)
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