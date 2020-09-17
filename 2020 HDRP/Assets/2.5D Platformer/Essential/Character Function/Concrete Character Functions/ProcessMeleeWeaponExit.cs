using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ProcessMeleeWeaponExit : CharacterFunction
    {
        public override void RunFunction(Collider col, TriggerDetector triggerDetector)
        {
            if (control.COLLIDING_OBJ_DATA.CollidingWeapons.ContainsKey(triggerDetector))
            {
                if (control.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Contains(col))
                {
                    control.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Remove(col);
                }

                if (control.COLLIDING_OBJ_DATA.CollidingWeapons[triggerDetector].Count == 0)
                {
                    control.COLLIDING_OBJ_DATA.CollidingWeapons.Remove(triggerDetector);
                }
            }
        }
    }
}