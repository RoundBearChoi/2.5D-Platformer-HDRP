using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Attack : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.ATTACK_DATA.AttackTriggered)
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