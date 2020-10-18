using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class TakeDamageFromThrownWeapon : CharacterFunction
    {
        public override void RunFunction(MeleeWeapon weapon, TriggerDetector triggerDetector)
        {
            AttackCondition info = new AttackCondition();
            info.CopyInfo(control.DATASET.DAMAGE_DATA.AxeThrow, control);

            control.DATASET.DAMAGE_DATA.damageTaken = new DamageTaken(
                weapon.Thrower,
                control.DATASET.DAMAGE_DATA.AxeThrow,
                triggerDetector,
                null,
                Vector3.zero);

            control.RunFunction(typeof(DamageReaction), info);

            if (weapon.FlyForward)
            {
                weapon.transform.rotation = Quaternion.Euler(0f, 90f, 45f);
            }
            else
            {
                weapon.transform.rotation = Quaternion.Euler(0f, -90f, 45f);
            }

            weapon.transform.parent = triggerDetector.transform;

            Vector3 offset = triggerDetector.transform.position - weapon.AxeTip.transform.position;
            weapon.transform.position += offset;

            weapon.IsThrown = false;
        }
    }
}