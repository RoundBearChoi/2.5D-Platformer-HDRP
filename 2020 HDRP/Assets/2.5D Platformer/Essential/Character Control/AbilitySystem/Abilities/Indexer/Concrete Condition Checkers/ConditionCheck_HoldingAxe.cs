using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_HoldingAxe : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.WEAPON_DATA.HoldingWeapon == null)
            {
                return false;
            }

            if (!control.WEAPON_DATA.HoldingWeapon.name.Contains("Axe"))
            {
                return false;
            }

            return true;
        }
    }
}