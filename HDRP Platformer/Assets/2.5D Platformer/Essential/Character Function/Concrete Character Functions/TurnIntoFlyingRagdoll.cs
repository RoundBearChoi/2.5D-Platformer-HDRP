using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class TurnIntoFlyingRagdoll : CharacterFunction
    {
        public override void RunFunction(AttackCondition info)
        {
            if (info.AttackAbility.collateralDamageInfo.CreateCollateral)
            {
                control.DATASET.RAGDOLL_DATA.flyingRagdollData.IsTriggered = true;
                control.DATASET.RAGDOLL_DATA.flyingRagdollData.Attacker = info.Attacker;
            }
        }
    }
}