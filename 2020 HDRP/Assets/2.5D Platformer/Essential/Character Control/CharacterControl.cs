using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    public enum RBScenes
    {
        TutorialScene_CharacterSelect,
        TutorialScene_Sample,
        TutorialScene_Sample_Night,
    }

    public class CharacterControl : MonoBehaviour
    {
        [Header("Input")]
        public bool Turbo;
        public bool MoveUp;
        public bool MoveDown;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Attack;
        public bool Block;

        // temp
        public AIProgress aiProgress;
        public AIController aiController;
        public BoxCollider boxCollider;
        public NavMeshObstacle navMeshObstacle;

        public CharacterSetup characterSetup;
        public CharacterData characterData;
        public CharacterFunctionProcessor characterFunctionProcessor;
        public CharacterUpdateProcessor characterUpdateProcessor;
        public CharacterQueryProcessor characterQueryProcessor;

        public GameObject RIGHT_HAND_ATTACK => characterSetup.attackPartSetup.RightHand_Attack;
        public GameObject LEFT_HAND_ATTACK => characterSetup.attackPartSetup.LeftHand_Attack;
        public GameObject RIGHT_FOOT_ATTACK => characterSetup.attackPartSetup.RightFoot_Attack;
        public GameObject LEFT_FOOT_ATTACK => characterSetup.attackPartSetup.LeftFoot_Attack;

        public BlockingObjData BLOCKING_DATA => characterData.blockingData;
        public LedgeGrabData LEDGE_GRAB_DATA => characterData.ledgeGrabData;
        public RagdollData RAGDOLL_DATA => characterData.ragdollData;
        public ManualInputData MANUAL_INPUT_DATA => characterData.manualInputData;
        public BoxColliderData BOX_COLLIDER_DATA => characterData.boxColliderData;
        public VerticalVelocityData VERTICAL_VELOCITY_DATA => characterData.verticalVelocityData;
        public DamageData DAMAGE_DATA => characterData.damageData;
        public MomentumData MOMENTUM_DATA => characterData.momentumData;
        public RotationData ROTATION_DATA => characterData.rotationData;
        public JumpData JUMP_DATA => characterData.jumpData;
        public CollisionSphereData COLLISION_SPHERE_DATA => characterData.collisionSphereData;
        public InstaKillData INSTA_KILL_DATA => characterData.instaKillData;
        public GroundData GROUND_DATA => characterData.groundData;
        public AttackData ATTACK_DATA => characterData.attackData;
        public AnimationData ANIMATION_DATA => characterData.animationData;
        public CollidingObjData COLLIDING_OBJ_DATA => characterData.collidingObjData;
        public WeaponData WEAPON_DATA => characterData.weaponData;
        public TurnData TURN_DATA => characterData.turnData;

        private Rigidbody rigid;

        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        public CharacterUpdate GetUpdater(System.Type UpdaterType)
        {
            if (characterUpdateProcessor.DicUpdaters.ContainsKey(UpdaterType))
            {
                return characterUpdateProcessor.DicUpdaters[UpdaterType];
            }
            else
            {
                return null;
            }
        }

        public void InitalizeCharacter()
        {
            RunFunction(typeof(InitCharacter), this);
        }


        public void CharacterUpdate()
        {
            characterUpdateProcessor.RunCharacterUpdate();
        }

        public void CharacterFixedUpdate()
        {
            characterUpdateProcessor.RunCharacterFixedUpdate();
        }

        public void CharacterLateUpdate()
        {
            characterUpdateProcessor.RunCharacterLateUpdate();
        }

        private void OnCollisionStay(Collision collision)
        {
            GROUND_DATA.BoxColliderContacts = collision.contacts;
        }

        public bool UpdatingAbility(System.Type abilityType)
        {
            return GetBool(typeof(CurrentAbility), abilityType);
        }

        public void RunFunction(System.Type CharacterFunctionType)
        {
            if (characterFunctionProcessor.DicFunctions.Count > 0)
            {
                characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction();
            }
        }

        public void RunFunction(System.Type CharacterFunctionType, float float1)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(float1);
        }

        public void RunFunction(System.Type CharacterFunctionType, float float1, float float2)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(float1, float2);
        }

        public void RunFunction(System.Type CharacterFunctionType, bool bool1)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(bool1);
        }

        public void RunFunction(System.Type CharacterFunctionType, CharacterControl characterControl, PoolObjectType poolObjType)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(characterControl, poolObjType);
        }

        public void RunFunction(System.Type CharacterFunctionType, RagdollPushType ragdollPushType)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(ragdollPushType);
        }

        public void RunFunction(System.Type CharacterFunctionType, GameObject obj)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(obj);
        }

        public void RunFunction(System.Type CharacterFunctionType, AttackCondition info)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(info);
        }

        public void RunFunction(System.Type CharacterFunctionType, MeleeWeapon weapon, TriggerDetector triggerDetector)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(weapon, triggerDetector);
        }

        public void RunFunction(System.Type CharacterFunctionType, Collider col, TriggerDetector triggerDetector)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(col, triggerDetector);
        }

        public void RunFunction(System.Type CharacterFunctionType, CharacterControl control)
        {
            if (characterFunctionProcessor == null)
            {
                characterFunctionProcessor = this.gameObject.GetComponentInChildren<CharacterFunctionProcessor>();
            }

            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction(control);
        }

        public bool GetBool(System.Type CharacterQueryType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnBool();
        }

        public bool GetBool(System.Type CharacterQueryType, string str)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnBool(str);
        }

        public bool GetBool(System.Type CharacterQueryType, System.Type paramType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnBool(paramType);
        }

        public bool GetBool(System.Type CharacterQueryType, AttackCondition info)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnBool(info);
        }

        public List<GameObject> GetGameObjList(System.Type CharacterQueryType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnGameObjList();
        }

        public GameObject GetGameObject(System.Type CharacterQueryType, AttackPartType attackPartType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnGameObj(attackPartType);
        }

        public GameObject GetGameObject(System.Type CharacterQueryType, string str)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnGameObj(str);
        }

        public MeleeWeapon GetMeleeWeapon(System.Type CharacterQueryType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnMeleeWeapon();
        }
    }
}