using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_TouchingWeapon : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.COLLIDING_OBJ_DATA.CollidingWeapons.Count == 0)
            {
                if (control.WEAPON_DATA.HoldingWeapon == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}