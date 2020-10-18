using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GetTouchingMeleeWeapon : CharacterQuery
    {
        public override MeleeWeapon ReturnMeleeWeapon()
        {
            foreach (KeyValuePair<TriggerDetector, List<Collider>> data in
                control.DATASET.COLLIDING_OBJ_DATA.CollidingWeapons)
            {
                MeleeWeapon w = data.Value[0].gameObject.GetComponent<MeleeWeapon>();
                return w;
            }

            return null;
        }
    }
}