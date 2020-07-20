using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterUpdateProcessor : MonoBehaviour
    {
        public CharacterUpdate[] ArrCharacterUpdate;
        public CharacterControl control;

        private void Awake()
        {
            ArrCharacterUpdate = new CharacterUpdate[(int)CharacterUpdateType.COUNT];
            control = GetComponentInParent<CharacterControl>();
        }

        public void RunCharacterFixedUpdate()
        {
            CharacterFixedUpdate(CharacterUpdateType.LEDGECHECKER);
            CharacterFixedUpdate(CharacterUpdateType.RAGDOLL);
            CharacterFixedUpdate(CharacterUpdateType.BLOCKINGOBJECTS);
            CharacterFixedUpdate(CharacterUpdateType.BOX_COLLIDER_UPDATER);
            CharacterFixedUpdate(CharacterUpdateType.VERTICAL_VELOCITY);
            CharacterFixedUpdate(CharacterUpdateType.COLLISION_SPHERES);
            CharacterFixedUpdate(CharacterUpdateType.INSTA_KILL);
            CharacterFixedUpdate(CharacterUpdateType.DAMAGE_DETECTOR);
            CharacterFixedUpdate(CharacterUpdateType.PLAYER_ROTATION);
        }

        public void RunCharacterUpdate()
        {
            CharacterUpdate(CharacterUpdateType.MANUALINPUT);
            CharacterUpdate(CharacterUpdateType.PLAYER_ATTACK);
            CharacterUpdate(CharacterUpdateType.PLAYER_ANIMATION);
        }

        void CharacterUpdate(CharacterUpdateType type)
        {
            if (ArrCharacterUpdate[(int)type] != null)
            {
                ArrCharacterUpdate[(int)type].OnUpdate();
            }
        }

        void CharacterFixedUpdate(CharacterUpdateType type)
        {
            if (ArrCharacterUpdate[(int)type] != null)
            {
                ArrCharacterUpdate[(int)type].OnFixedUpdate();
            }
        }
    }
}
