using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/AI/RestartAI")]
    public class RestartAI : CharacterAbility
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!AIIsOnGround(characterState.characterControl))
            {
                return;
            }

            if (characterState.AI_CONTROLLER.RestartWalk())
            {
                characterState.AI_CONTROLLER.InitializeAI();
            }

            if (characterState.UpdatingAbility(typeof(Landing)))
            {
                characterState.characterControl.Turbo = false;
                characterState.characterControl.Jump = false;
                characterState.characterControl.MoveUp = false;
                characterState.characterControl.MoveLeft = false;
                characterState.characterControl.MoveRight = false;
                characterState.characterControl.MoveDown = false;
                characterState.characterControl.aiController.InitializeAI();
            }

            if (characterState.AI_CONTROLLER.IsAttacking())
            {
                if (characterState.characterControl.aiProgress.AIDistanceToTarget() > 3f ||
                    !characterState.characterControl.aiProgress.TargetIsOnSamePlatform())
                {
                    characterState.characterControl.Turbo = false;
                    characterState.characterControl.Jump = false;
                    characterState.characterControl.MoveUp = false;
                    characterState.characterControl.MoveLeft = false;
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveDown = false;
                    characterState.characterControl.aiController.InitializeAI();
                }
            }

            // path is blocked
            if (characterState.BLOCKING_DATA.FrontBlockingDicCount == 0)
            {
                characterState.characterControl.aiProgress.BlockingCharacter = null;
            }
            else
            {
                List<GameObject> objs = characterState.characterControl.GetGameObjList(typeof(FrontBlockingCharacterList));

                foreach(GameObject o in objs)
                {
                    CharacterControl blockingChar = CharacterManager.Instance.GetCharacter(o);

                    if (blockingChar != null)
                    {
                        characterState.characterControl.aiProgress.BlockingCharacter = blockingChar;
                        break;
                    }
                    else
                    {
                        characterState.characterControl.aiProgress.BlockingCharacter = null;
                    }
                }
            }

            if (characterState.characterControl.aiProgress.BlockingCharacter != null)
            {
                if (characterState.GROUND_DATA.Ground != null)
                {
                    if (!characterState.UpdatingAbility(typeof(Jump)) &&
                        !characterState.UpdatingAbility(typeof(JumpPrep)))
                    {
                        characterState.characterControl.Turbo = false;
                        characterState.characterControl.Jump = false;
                        characterState.characterControl.MoveUp = false;
                        characterState.characterControl.MoveLeft = false;
                        characterState.characterControl.MoveRight = false;
                        characterState.characterControl.MoveDown = false;
                        characterState.characterControl.aiController.InitializeAI();
                    }
                }
            }

            //startsphere height
            if (characterState.GROUND_DATA.Ground != null &&
                !characterState.UpdatingAbility(typeof(Jump)) &&
                !characterState.UpdatingAbility(typeof(WallJumpPrep)))
            {
                if (characterState.characterControl.aiProgress.GetStartSphereHeight() > 0.1f)
                {
                    characterState.characterControl.Turbo = false;
                    characterState.characterControl.Jump = false;
                    characterState.characterControl.MoveUp = false;
                    characterState.characterControl.MoveLeft = false;
                    characterState.characterControl.MoveRight = false;
                    characterState.characterControl.MoveDown = false;
                    characterState.characterControl.aiController.InitializeAI();
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool AIIsOnGround(CharacterControl control)
        {
            if (control.UpdatingAbility(typeof(MoveUp)))
            {
                return false;
            }

            if (control.RIGID_BODY.useGravity)
            {
                if (control.characterSetup.SkinnedMeshAnimator.GetBool(
                    HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}