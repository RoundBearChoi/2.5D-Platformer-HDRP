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
            if (!AIIsOnGround(characterState.control))
            {
                return;
            }

            if (characterState.AI_CONTROLLER.RestartWalk())
            {
                characterState.AI_CONTROLLER.InitializeAI();
            }

            if (characterState.UpdatingAbility(typeof(Landing)))
            {
                characterState.control.Turbo = false;
                characterState.control.Jump = false;
                characterState.control.MoveUp = false;
                characterState.control.MoveLeft = false;
                characterState.control.MoveRight = false;
                characterState.control.MoveDown = false;
                characterState.control.aiController.InitializeAI();
            }

            if (characterState.AI_CONTROLLER.IsAttacking())
            {
                if (characterState.control.aiProgress.AIDistanceToTarget() > 3f ||
                    !characterState.control.aiProgress.TargetIsOnSamePlatform())
                {
                    characterState.control.Turbo = false;
                    characterState.control.Jump = false;
                    characterState.control.MoveUp = false;
                    characterState.control.MoveLeft = false;
                    characterState.control.MoveRight = false;
                    characterState.control.MoveDown = false;
                    characterState.control.aiController.InitializeAI();
                }
            }

            // path is blocked
            if (characterState.control.BLOCKING_DATA.FrontBlockingDicCount == 0)
            {
                characterState.control.aiProgress.BlockingCharacter = null;
            }
            else
            {
                List<GameObject> objs = characterState.control.GetGameObjList(typeof(FrontBlockingCharacterList));

                foreach(GameObject o in objs)
                {
                    CharacterControl blockingChar = CharacterManager.Instance.GetCharacter(o);

                    if (blockingChar != null)
                    {
                        characterState.control.aiProgress.BlockingCharacter = blockingChar;
                        break;
                    }
                    else
                    {
                        characterState.control.aiProgress.BlockingCharacter = null;
                    }
                }
            }

            if (characterState.control.aiProgress.BlockingCharacter != null)
            {
                if (characterState.GROUND_DATA.Ground != null)
                {
                    if (!characterState.UpdatingAbility(typeof(Jump)) &&
                        !characterState.UpdatingAbility(typeof(JumpPrep)))
                    {
                        characterState.control.Turbo = false;
                        characterState.control.Jump = false;
                        characterState.control.MoveUp = false;
                        characterState.control.MoveLeft = false;
                        characterState.control.MoveRight = false;
                        characterState.control.MoveDown = false;
                        characterState.control.aiController.InitializeAI();
                    }
                }
            }

            //startsphere height
            if (characterState.GROUND_DATA.Ground != null &&
                !characterState.UpdatingAbility(typeof(Jump)) &&
                !characterState.UpdatingAbility(typeof(WallJumpPrep)))
            {
                if (characterState.control.aiProgress.GetStartSphereHeight() > 0.1f)
                {
                    characterState.control.Turbo = false;
                    characterState.control.Jump = false;
                    characterState.control.MoveUp = false;
                    characterState.control.MoveLeft = false;
                    characterState.control.MoveRight = false;
                    characterState.control.MoveDown = false;
                    characterState.control.aiController.InitializeAI();
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