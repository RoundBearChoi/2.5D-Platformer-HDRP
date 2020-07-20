using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AddForceToDamagedPart : CharacterFunction
    {
        public override void RunFunction(RagdollPushType ragdollPushType)
        {
            if (control.DAMAGE_DATA.damageTaken != null)
            {
                if (control.DAMAGE_DATA.damageTaken.ATTACKER != null)
                {
                    ProcAddForce(ragdollPushType);
                }
            }
        }

        void ProcAddForce(RagdollPushType pushType)
        {
            DamageData damageData = control.DAMAGE_DATA;

            Vector3 forwardDir = damageData.damageTaken.ATTACKER.transform.forward;
            Vector3 rightDir = damageData.damageTaken.ATTACKER.transform.right;
            Vector3 upDir = damageData.damageTaken.ATTACKER.transform.up;

            Rigidbody body = control.DAMAGE_DATA.damageTaken.DAMAGEE.GetComponent<Rigidbody>();
            Attack attack = damageData.damageTaken.ATTACK;

            if (pushType == RagdollPushType.NORMAL)
            {
                body.AddForce(
                    forwardDir * attack.normalRagdollVelocity.ForwardForce +
                    rightDir * attack.normalRagdollVelocity.RightForce +
                    upDir * attack.normalRagdollVelocity.UpForce);
            }
            else if (pushType == RagdollPushType.DEAD_BODY)
            {
                body.AddForce(
                    forwardDir * attack.collateralRagdollVelocity.ForwardForce +
                    rightDir * attack.collateralRagdollVelocity.RightForce +
                    upDir * attack.collateralRagdollVelocity.UpForce);
            }
        }
    }
}