using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class IsCollidingWithAttack : CharacterQuery
    {
        public override bool ReturnBool(AttackCondition info)
        {
            foreach (KeyValuePair<TriggerDetector, List<Collider>> data in
                control.COLLIDING_OBJ_DATA.CollidingBodyParts)
            {
                foreach (Collider collider in data.Value)
                {
                    foreach (AttackPartType part in info.AttackParts)
                    {
                        GameObject attackingPart = info.Attacker.GetGameObject(typeof(GetAttackingPart), part);

                        if (attackingPart == collider.gameObject)
                        {
                            control.DAMAGE_DATA.damageTaken = new DamageTaken(
                                info.Attacker,
                                info.AttackAbility,
                                data.Key,
                                attackingPart,
                                Vector3.zero);

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}