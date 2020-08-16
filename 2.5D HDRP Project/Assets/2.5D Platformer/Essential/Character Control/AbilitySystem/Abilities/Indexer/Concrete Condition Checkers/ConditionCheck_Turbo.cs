using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Turbo : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.Turbo)
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