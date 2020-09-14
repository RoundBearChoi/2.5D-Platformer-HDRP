using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DamageDetector : CharacterUpdate
    {
        public override void InitComponent()
        {
            control.DAMAGE_DATA.TakeDamage = ProcessDamage;
        }

        public override void OnFixedUpdate()
        {
            if (AttackManager.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }

        void CheckAttack()
        {
            foreach (AttackCondition info in AttackManager.Instance.CurrentAttacks)
            {
                if (control.GetBool(typeof(AttackIsValid), info))
                {
                    if (info.MustCollide)
                    {
                        if (control.COLLIDING_OBJ_DATA.CollidingBodyParts.Count != 0)
                        {
                            if (control.GetBool(typeof(IsCollidingWithAttack), info))
                            {
                                ProcessDamage(info);
                            }
                        }
                    }
                    else
                    {
                        if (IsInLethalRange(info))
                        {
                            ProcessDamage(info);
                        }
                    }
                }
            }
        }

        bool IsInLethalRange(AttackCondition info)
        {
            for (int i = 0; i < control.RAGDOLL_DATA.ArrBodyParts.Length; i++)
            {
                float dist = Vector3.SqrMagnitude(
                    control.RAGDOLL_DATA.ArrBodyParts[i].transform.position - info.Attacker.transform.position);

                if (dist <= info.LethalRange)
                {
                    int index = Random.Range(0, control.RAGDOLL_DATA.ArrBodyParts.Length);
                    TriggerDetector triggerDetector = control.RAGDOLL_DATA.ArrBodyParts[index].GetComponent<TriggerDetector>();

                    control.DAMAGE_DATA.damageTaken = new DamageTaken(
                        info.Attacker,
                        info.AttackAbility,
                        triggerDetector,
                        null,
                        Vector3.zero);

                    return true;
                }
            }

            return false;
        }

        void ProcessDamage(AttackCondition info)
        {
            if (control.GetBool(typeof(CharacterDead)))
            {
                control.RunFunction(typeof(GetPushedAsRagdoll), info);
            }
            else
            {
                if (!control.GetBool(typeof(BlockedAttack), info))
                {
                    control.RunFunction(typeof(TakeDamage), info);
                }
            }
        }
    }
}