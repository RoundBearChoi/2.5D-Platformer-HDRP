using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class InitCharacter : CharacterFunction
    {
        public override void RunFunction(CharacterControl control)
        {
            control.characterUpdateProcessor = control.GetComponentInChildren<CharacterUpdateProcessor>();
            control.characterSetup = control.GetComponentInChildren<CharacterSetup>();
            control.characterQueryProcessor = control.GetComponentInChildren<CharacterQueryProcessor>();

            RegisterCharacter();
            InitCharacterStates(control.ANIMATOR);
        }

        void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(control))
            {
                CharacterManager.Instance.Characters.Add(control);
            }
        }
        
        void InitCharacterStates(Animator animator)
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach (CharacterState c in arr)
            {
                c.control = control;
            }
        }
    }
}