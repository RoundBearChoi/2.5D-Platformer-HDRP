using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationProgress : MonoBehaviour
    {
        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        public MeleeWeapon GetTouchingWeapon()
        {
            foreach(KeyValuePair<TriggerDetector, List<Collider>> data in control.COLLIDING_OBJ_DATA.CollidingWeapons)
            {
                MeleeWeapon w = data.Value[0].gameObject.GetComponent<MeleeWeapon>();
                return w;
            }

            return null;
        }
    }
}