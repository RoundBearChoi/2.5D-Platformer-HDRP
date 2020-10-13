using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ProcessMeleeWeaponContact : CharacterFunction
    {
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
                if (!control.COLLIDING_OBJ_DATA.CollidingWeapons.ContainsKey(triggerDetector))
                {
                    control.COLLIDING_OBJ_DATA.CollidingWeapons.Add(triggerDetector, new List<Collider>());
                }

                if (!control.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Contains(col))
                {
                    control.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Add(col);
                }
            }
        }
    }
}