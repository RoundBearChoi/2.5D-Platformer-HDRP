using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class SpawnHitParticles : CharacterFunction
    {
        public override void RunFunction(CharacterControl attacker, PoolObjectType EffectsType)
        {
            GameObject vfx = PoolManager.Instance.GetObject(EffectsType);

            vfx.transform.position =
                control.DAMAGE_DATA.damageTaken.DAMAGEE.triggerCollider.bounds.center;

            vfx.SetActive(true);

            if (attacker.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
            {
                vfx.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                vfx.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
}