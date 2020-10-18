using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GetAttackingPart : CharacterQuery
    {
        public override GameObject ReturnGameObj(AttackPartType attackPartType)
        {
            if (attackPartType == AttackPartType.LEFT_HAND)
            {
                return control.LEFT_HAND_ATTACK;
            }
            else if (attackPartType == AttackPartType.RIGHT_HAND)
            {
                return control.RIGHT_HAND_ATTACK;
            }
            else if (attackPartType == AttackPartType.LEFT_FOOT)
            {
                return control.LEFT_FOOT_ATTACK;
            }
            else if (attackPartType == AttackPartType.RIGHT_FOOT)
            {
                return control.RIGHT_FOOT_ATTACK;
            }
            else if (attackPartType == AttackPartType.MELEE_WEAPON)
            {
                return control.DATASET.WEAPON_DATA.HoldingWeapon.triggerDetector.gameObject;
            }

            return null;
        }
    }
}