﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterState : StateMachineBehaviour
    {
        public CharacterControl characterControl;

        [Space(10)]
        public List<CharacterAbility> ListAbilityData = new List<CharacterAbility>();
        [Space(10)]
        public CharacterAbility[] ArrAbilities;
        [Space(20)]
        [Header("Separated Arrays")]
        public CharacterAbility[] ArrMove;
        [Space(10)]
        public CharacterAbility[] ArrTransition;
        [Space(10)]
        public CharacterAbility[] ArrAttack;
        [Space(10)]
        public CharacterAbility[] ArrOther;

        public GameObject RIGHT_HAND_ATTACK => characterControl.characterSetup.attackPartSetup.RightHand_Attack;
        //public GameObject LEFT_HAND_ATTACK => characterControl.characterSetup.attackPartSetup.LeftHand_Attack;
        //public GameObject RIGHT_FOOT_ATTACK => characterControl.characterSetup.attackPartSetup.RightFoot_Attack;
        //public GameObject LEFT_FOOT_ATTACK => characterControl.characterSetup.attackPartSetup.LeftFoot_Attack;

        public BlockingObjData BLOCKING_DATA => characterControl.characterData.blockingData;
        public RagdollData RAGDOLL_DATA => characterControl.characterData.ragdollData;
        public BoxColliderData BOX_COLLIDER_DATA => characterControl.characterData.boxColliderData;
        public VerticalVelocityData VERTICAL_VELOCITY_DATA => characterControl.characterData.verticalVelocityData;
        public MomentumData MOMENTUM_DATA => characterControl.characterData.momentumData;
        public RotationData ROTATION_DATA => characterControl.characterData.rotationData;
        public JumpData JUMP_DATA => characterControl.characterData.jumpData;
        public CollisionSphereData COLLISION_SPHERE_DATA => characterControl.characterData.collisionSphereData;
        public GroundData GROUND_DATA => characterControl.characterData.groundData;
        public AttackData ATTACK_DATA => characterControl.characterData.attackData;
        public AnimationData ANIMATION_DATA => characterControl.characterData.animationData;
        public AIController AI_CONTROLLER => characterControl.aiController;
        public WeaponData WEAPON_DATA => characterControl.characterData.weaponData;
        public CameraData CAMERA_DATA => characterControl.characterData.cameraData;
        public TurnData TURN_DATA => characterControl.characterData.turnData;

        public void PutStatesInArray()
        {
            ArrAbilities = new CharacterAbility[ListAbilityData.Count];
            
            for(int i = 0; i < ListAbilityData.Count; i++)
            {
                ArrAbilities[i] = ListAbilityData[i];
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EnterAll(this, animator, stateInfo, ArrAbilities);

            EnterAll(this, animator, stateInfo, ArrMove);
            EnterAll(this, animator, stateInfo, ArrTransition);
            EnterAll(this, animator, stateInfo, ArrAttack);
            EnterAll(this, animator, stateInfo, ArrOther);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo, ArrAbilities);

            UpdateAll(this, animator, stateInfo, ArrMove);
            UpdateAll(this, animator, stateInfo, ArrTransition);
            UpdateAll(this, animator, stateInfo, ArrAttack);
            UpdateAll(this, animator, stateInfo, ArrOther);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            ExitAll(this, animator, stateInfo, ArrAbilities);

            ExitAll(this, animator, stateInfo, ArrMove);
            ExitAll(this, animator, stateInfo, ArrTransition);
            ExitAll(this, animator, stateInfo, ArrAttack);
            ExitAll(this, animator, stateInfo, ArrOther);
        }

        public void EnterAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, CharacterAbility[] AbilityList)
        {
            for (int i = 0; i < AbilityList.Length; i++)
            {
                AbilityList[i].OnEnter(characterState, animator, stateInfo);

                if (characterControl.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(AbilityList[i]))
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities[AbilityList[i]] += 1;
                }
                else
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities.Add(AbilityList[i], 1);
                }
            }
        }

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, CharacterAbility[] AbilityList)
        {
            for (int i = 0; i < AbilityList.Length; i++)
            {
                AbilityList[i].UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public void ExitAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, CharacterAbility[] AbilityList)
        {
            for (int i = 0; i < AbilityList.Length; i++)
            {
                AbilityList[i].OnExit(characterState, animator, stateInfo);

                if (characterControl.ANIMATION_DATA.CurrentRunningAbilities.ContainsKey(AbilityList[i]))
                {
                    characterControl.ANIMATION_DATA.CurrentRunningAbilities[AbilityList[i]] -= 1;

                    if (characterControl.ANIMATION_DATA.CurrentRunningAbilities[AbilityList[i]] <= 0)
                    {
                        characterControl.ANIMATION_DATA.CurrentRunningAbilities.Remove(AbilityList[i]);
                    }
                }
            }
        }
    }
}