using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class SubComponentProcessor : MonoBehaviour
    {
        public SubComponent[] ArrSubComponents;
        public CharacterControl control;

        private void Awake()
        {
            ArrSubComponents = new SubComponent[(int)SubComponentType.COUNT];
            control = GetComponentInParent<CharacterControl>();
        }

        public void FixedUpdateSubComponents()
        {
            FixedUpdateSubComponent(SubComponentType.LEDGECHECKER);
            FixedUpdateSubComponent(SubComponentType.RAGDOLL);
            FixedUpdateSubComponent(SubComponentType.BLOCKINGOBJECTS);
            FixedUpdateSubComponent(SubComponentType.BOX_COLLIDER_UPDATER);
            FixedUpdateSubComponent(SubComponentType.VERTICAL_VELOCITY);
            FixedUpdateSubComponent(SubComponentType.COLLISION_SPHERES);
            FixedUpdateSubComponent(SubComponentType.INSTA_KILL);
            FixedUpdateSubComponent(SubComponentType.DAMAGE_DETECTOR);
            FixedUpdateSubComponent(SubComponentType.PLAYER_ROTATION);
        }

        public void UpdateSubComponents()
        {
            UpdateSubComponent(SubComponentType.MANUALINPUT);
            UpdateSubComponent(SubComponentType.PLAYER_ATTACK);
            UpdateSubComponent(SubComponentType.PLAYER_ANIMATION);
        }

        void UpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnUpdate();
            }
        }

        void FixedUpdateSubComponent(SubComponentType type)
        {
            if (ArrSubComponents[(int)type] != null)
            {
                ArrSubComponents[(int)type].OnFixedUpdate();
            }
        }
    }
}
