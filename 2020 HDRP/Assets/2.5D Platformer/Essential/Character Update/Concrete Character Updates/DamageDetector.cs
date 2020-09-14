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

        bool AttackIsValid(AttackCondition info)
        {
            if (info == null)
            {
                return false;
            }

            if (!info.isRegisterd)
            {
                return false;
            }

            if (info.isFinished)
            {
                return false;
            }

            if (info.CurrentHits >= info.MaxHits)
            {
                return false;
            }

            if (info.Attacker == control)
            {
                return false;
            }

            if (info.MustFaceAttacker)
            {
                Vector3 vec = this.transform.position - info.Attacker.transform.position;
                if (vec.z * info.Attacker.transform.forward.z < 0f)
                {
                    return false;
                }
            }

            if (info.RegisteredTargets.Contains(this.control))
            {
                return false;
            }

            return true;
        }

        void CheckAttack()
        {
            foreach (AttackCondition info in AttackManager.Instance.CurrentAttacks)
            {
                if (AttackIsValid(info))
                {
                    if (info.MustCollide)
                    {
                        if (control.COLLIDING_OBJ_DATA.CollidingBodyParts.Count != 0)
                        {
                            if (IsCollided(info))
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

        bool IsCollided(AttackCondition info)
        {
            foreach(KeyValuePair<TriggerDetector, List<Collider>> data in
                control.COLLIDING_OBJ_DATA.CollidingBodyParts)
            {
                foreach(Collider collider in data.Value)
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

        bool IsBlocked(AttackCondition info)
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

        void ProcessDamage(AttackCondition info)
        {
            if (control.GetBool(typeof(CharacterDead)))
            {
                control.RunFunction(typeof(GetPushedAsRagdoll), info);
            }
            else
            {
                if (!IsBlocked(info))
                {
                    control.RunFunction(typeof(TakeDamage), info);
                }
            }
        }
    }
}