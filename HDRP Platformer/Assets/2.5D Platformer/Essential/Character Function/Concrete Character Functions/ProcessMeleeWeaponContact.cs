using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ProcessMeleeWeaponContact : CharacterFunction
    {
        CollidingObjData COLLIDING_OBJS => control.DATASET.COLLIDING_OBJ_DATA;

        public override void RunFunction(Collider col, TriggerDetector triggerDetector)
        {
            MeleeWeapon w = col.transform.root.gameObject.GetComponent<MeleeWeapon>();

            if (w == null)
            {
                return;
            }

            if (w.IsThrown &&
                w.Thrower != control)
            {
                control.RunFunction(typeof(TakeDamageFromThrownWeapon), w, triggerDetector);
            }
            else
            {
                if (!COLLIDING_OBJS.CollidingWeapons.ContainsKey(triggerDetector))
                {
                    COLLIDING_OBJS.CollidingWeapons.Add(triggerDetector, new List<Collider>());
                }

                if (!COLLIDING_OBJS.CollidingWeapons[triggerDetector].Contains(col))
                {
                    COLLIDING_OBJS.CollidingWeapons[triggerDetector].Add(col);
                }
            }
        }
    }
}