using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_AttackIsBlocked : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.DATASET.DAMAGE_DATA.BlockedAttack == null)
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