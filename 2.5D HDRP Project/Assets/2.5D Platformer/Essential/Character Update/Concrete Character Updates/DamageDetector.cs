using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DamageDetector : CharacterUpdate
    {
        static string VFX = "VFX";

        public override void InitComponent()
        {
            control.DAMAGE_DATA.IsDead = IsDead;
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

        bool IsDead()
        {
            if (control.DAMAGE_DATA.hp <= 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool IsBlocked(AttackCondition info)
        {
            if (info == control.DAMAGE_DATA.BlockedAttack && control.DAMAGE_DATA.BlockedAttack != null)
            {
                return true;
            }

            if (control.ANIMATION_DATA.IsRunning(typeof(Block)))
            {
                Vector3 dir = info.Attacker.transform.position - control.transform.position;

                if (dir.z > 0f)
                {
                    if (control.ROTATION_DATA.IsFacingForward())
                    {
                        return true;
                    }
                }
                else if (dir.z < 0f)
                {
                    if (!control.ROTATION_DATA.IsFacingForward())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        void ProcessDamage(AttackCondition info)
        {
            if (IsDead())
            {
                PushDeadBody(info);
            }
            else
            {
                if (!IsBlocked(info))
                {
                    TakeDamage(info);
                }
            }
        }

        void PushDeadBody(AttackCondition info)
        {
            if (!info.RegisteredTargets.Contains(this.control))
            {
                if (info.AttackAbility.collateralDamageInfo.CreateCollateral)
                {
                    control.RunFunction(typeof(SpawnHitParticles), info.Attacker, info.AttackAbility.ParticleType);
                    ProcessFlyingRagdoll(info);
                }

                info.RegisteredTargets.Add(this.control);

                control.RunFunction(typeof(ClearAllVelocity));
                control.RunFunction(typeof(AddForceToDamagedPart), RagdollPushType.DEAD_BODY);
            }

            return;
        }

        void TakeDamage(AttackCondition info)
        {
            ProcessHitParticles(info);

            info.CurrentHits++;
            control.DAMAGE_DATA.hp -= info.AttackAbility.Damage;

            AttackManager.Instance.ForceDeregister(control);
            control.ANIMATION_DATA.CurrentRunningAbilities.Clear();

            if (IsDead())
            {
                control.RAGDOLL_DATA.RagdollTriggered = true;
            }
            else
            {
                int randomIndex = Random.Range(0, (int)Hit_Reaction_States.COUNT);

                control.characterSetup.
                    SkinnedMeshAnimator.Play(
                    HashManager.Instance.DicHitReactionStates[
                        (Hit_Reaction_States)randomIndex], 0, 0f);
            }

            ProcessFlyingRagdoll(info);

            if (!info.RegisteredTargets.Contains(this.control))
            {
                info.RegisteredTargets.Add(this.control);
            }
        }

        void ProcessHitParticles(AttackCondition info)
        {
            if (info.MustCollide)
            {
                CameraManager.Instance.ShakeCamera(0.3f);

                if (info.AttackAbility.UseDeathParticles)
                {
                    if (info.AttackAbility.ParticleType.ToString().Contains(VFX))
                    {
                        control.RunFunction(typeof(SpawnHitParticles), info.Attacker, info.AttackAbility.ParticleType);
                    }
                }
            }
        }


        void ProcessFlyingRagdoll(AttackCondition info)
        {
            if (info.AttackAbility.collateralDamageInfo.CreateCollateral)
            {
                control.RAGDOLL_DATA.flyingRagdollData.IsTriggered = true;
                control.RAGDOLL_DATA.flyingRagdollData.Attacker = info.Attacker;
            }
        }
    }
}