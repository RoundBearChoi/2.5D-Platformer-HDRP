using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_AttackIsBlocked : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.DAMAGE_DATA.BlockedAttack == null)
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