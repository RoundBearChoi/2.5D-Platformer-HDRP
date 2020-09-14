using UnityEngine;

namespace Roundbeargames
{
    public class GetPushedAsRagdoll : CharacterFunction
    {
        public override void RunFunction(AttackCondition info)
        {
            if (!info.RegisteredTargets.Contains(this.control))
            {
                if (info.AttackAbility.collateralDamageInfo.CreateCollateral)
                {
                    control.RunFunction(typeof(SpawnHitParticles), info.Attacker, info.AttackAbility.ParticleType);
                    control.RunFunction(typeof(TurnIntoFlyingRagdoll), info);
                }

                info.RegisteredTargets.Add(this.control);

                control.RunFunction(typeof(ClearRagdollVelocity));
                control.RunFunction(typeof(AddForceToDamagedPart), RagdollPushType.DEAD_BODY);
            }
        }
    }
}