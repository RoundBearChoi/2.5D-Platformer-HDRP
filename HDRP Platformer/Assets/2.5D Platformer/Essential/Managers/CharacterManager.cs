using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> Characters = new List<CharacterControl>();

        [SerializeField]
        CharacterControl[] ArrCharacters = null;

        private void Update()
        {
            InitCharacterArray();

            for (int i = 0; i < ArrCharacters.Length; i++)
            {
                ArrCharacters[i].CharacterUpdate();
            }
        }

        private void FixedUpdate()
        {
            InitCharacterArray();

            for (int i = 0; i < ArrCharacters.Length; i++)
            {
                ArrCharacters[i].CharacterFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            InitCharacterArray();

            for (int i = 0; i < ArrCharacters.Length; i++)
            {
                ArrCharacters[i].CharacterLateUpdate();
            }
        }

        void InitCharacterArray()
        {
            if (ArrCharacters == null)
            {
                ArrCharacters = new CharacterControl[Characters.Count];

                for (int i = 0; i < Characters.Count; i++)
                {
                    ArrCharacters[i] = Characters[i];
                }
            }
        }

        public CharacterControl GetCharacter(PlayableCharacterType playableCharacterType)
        {
            for (int i = 0; i < ArrCharacters.Length; i++)
            {
                if (ArrCharacters[i].characterSetup.playableCharacterType == playableCharacterType)
                {
                    return ArrCharacters[i];
                }
            }

            return null;
        }

        public CharacterControl GetCharacter(Animator animator)
        {
            for (int i = 0; i < ArrCharacters.Length; i++)
            {
                if (ArrCharacters[i].ANIMATOR == animator)
                {
                    return ArrCharacters[i];
                }
            }

            return null;
        }

        public CharacterControl GetCharacter(GameObject obj)
        {
            for (int i = 0; i < ArrCharacters.Length; i++)
            {
                if (ArrCharacters[i].gameObject == obj)
                {
                    return ArrCharacters[i];
                }
            }

            return null;
        }

        public CharacterControl GetPlayableCharacter()
        {
            for (int i = 0; i < ArrCharacters.Length; i++)
            {
                if (ArrCharacters[i].GetUpdater(typeof(ManualInput)) != null)
                {
                    return ArrCharacters[i];
                }
            }

            return null;
        }
    }
}