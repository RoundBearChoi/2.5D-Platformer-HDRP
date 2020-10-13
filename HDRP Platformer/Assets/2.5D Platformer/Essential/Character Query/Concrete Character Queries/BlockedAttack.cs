using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BlockedAttack : CharacterQuery
    {
        public override bool ReturnBool(AttackCondition info)
        {
            if (info == control.DAMAGE_DATA.BlockedAttack && control.DAMAGE_DATA.BlockedAttack != null)
            {
                return true;
            }

            if (control.UpdatingAbility(typeof(Block)))
            {
                Vector3 dir = info.Attacker.transform.position - control.transform.position;

                if (dir.z > 0f)
                {
                    if (control.GetBool(typeof(FacingForward)))
                    {
                        return true;
                    }
                }
                else if (dir.z < 0f)
                {
                    if (!control.GetBool(typeof(FacingForward)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}