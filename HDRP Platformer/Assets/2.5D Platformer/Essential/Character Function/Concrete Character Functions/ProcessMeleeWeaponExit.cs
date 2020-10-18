using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ProcessMeleeWeaponExit : CharacterFunction
    {
        public override void RunFunction(Collider col, TriggerDetector triggerDetector)
        {
            if (control.DATASET.COLLIDING_OBJ_DATA.CollidingWeapons.ContainsKey(triggerDetector))
            {
                if (control.DATASET.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Contains(col))
                {
                    control.DATASET.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Remove(col);
                }

                if (control.DATASET.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Count == 0)
                {
                    control.DATASET.COLLIDING_OBJ_DATA.CollidingWeapons.Remove(triggerDetector);
                }
            }
        }
    }
}