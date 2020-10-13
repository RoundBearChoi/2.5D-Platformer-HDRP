using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class TakeDamage : CharacterFunction
    {
        public override void RunFunction(AttackCondition info)
        {
            if (control.GetBool(typeof(ShouldShowHitParticles), info))
            {
                control.RunFunction(typeof(SpawnHitParticles), info.Attacker, info.AttackAbility.ParticleType);
            }

            info.CurrentHits++;
            control.DAMAGE_DATA.hp -= info.AttackAbility.Damage;

            AttackManager.Instance.ForceDeregister(control);
            control.ANIMATION_DATA.CurrentRunningAbilities.Clear();

            if (control.GetBool(typeof(CharacterDead)))
            {
                control.RAGDOLL_DATA.RagdollTriggered = true;
            }
            else
            {
                int randomIndex = Random.Range(0, HashTool.GetLength(typeof(Hit_Reaction_States)));

                control.characterSetup.
                    SkinnedMeshAnimator.Play(
                    HashManager.Instance.DicHitReactionStates[
                        (Hit_Reaction_States)randomIndex], 0, 0f);
            }

            control.RunFunction(typeof(TurnIntoFlyingRagdoll), info);

            if (!info.RegisteredTargets.Contains(this.control))
            {
                info.RegisteredTargets.Add(this.control);
            }
        }
    }
}