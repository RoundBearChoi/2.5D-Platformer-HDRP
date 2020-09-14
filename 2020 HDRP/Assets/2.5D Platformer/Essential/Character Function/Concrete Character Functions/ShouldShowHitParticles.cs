using UnityEngine;

namespace Roundbeargames
{
    public class ShouldShowHitParticles : CharacterQuery
    {
        static string VFX = "VFX";

        public override bool ReturnBool(AttackCondition info)
        {
            if (info.MustCollide)
            {
                CameraManager.Instance.ShakeCamera(0.3f);

                if (info.AttackAbility.UseDeathParticles)
                {
                    if (info.AttackAbility.ParticleType.ToString().Contains(VFX))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}