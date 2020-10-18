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

            // temp
            control.aiProgress = control.GetComponentInChildren<AIProgress>();
            control.boxCollider = control.GetComponent<BoxCollider>();
            control.navMeshObstacle = control.GetComponent<UnityEngine.AI.NavMeshObstacle>();

            control.aiController = control.GetComponentInChildren<AIController>();
            if (control.aiController == null)
            {
                if (control.navMeshObstacle != null)
                {
                    control.navMeshObstacle.carving = true;
                }
            }

            control.characterSetup = control.GetComponentInChildren<CharacterSetup>();
            control.characterData = control.GetComponentInChildren<CharacterData>();
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