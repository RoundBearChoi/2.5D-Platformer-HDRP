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
                return control.LeftHand_Attack;
            }
            else if (attackPartType == AttackPartType.RIGHT_HAND)
            {
                return control.RightHand_Attack;
            }
            else if (attackPartType == AttackPartType.LEFT_FOOT)
            {
                return control.LeftFoot_Attack;
            }
            else if (attackPartType == AttackPartType.RIGHT_FOOT)
            {
                return control.RightFoot_Attack;
            }
            else if (attackPartType == AttackPartType.MELEE_WEAPON)
            {
                return control.animationProgress.HoldingWeapon.triggerDetector.gameObject;
            }

            return null;
        }
    }
}