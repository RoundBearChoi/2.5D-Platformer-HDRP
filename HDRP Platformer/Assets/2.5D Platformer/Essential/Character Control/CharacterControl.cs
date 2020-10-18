using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
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

        [Header("Disable Turning")]
        public bool DisableTurning;

        [Header("temp")]
        public BoxCollider boxCollider;

        [Header("SubComponents")]
        public CharacterSetup characterSetup;
        public CharacterFunctionProcessor characterFunctionProcessor;
        public CharacterUpdateProcessor characterUpdateProcessor;
        public CharacterQueryProcessor characterQueryProcessor;

        public GameObject RIGHT_HAND_ATTACK => characterSetup.attackPartSetup.RightHand_Attack;
        public GameObject LEFT_HAND_ATTACK => characterSetup.attackPartSetup.LeftHand_Attack;
        public GameObject RIGHT_FOOT_ATTACK => characterSetup.attackPartSetup.RightFoot_Attack;
        public GameObject LEFT_FOOT_ATTACK => characterSetup.attackPartSetup.LeftFoot_Attack;

        [Header("Character Data")]
        public MoveData MOVE_DATA;
        [Space(3)] public DamageData DAMAGE_DATA;
        [Space(3)] public BlockingObjData BLOCKING_DATA;
        [Space(3)] public LedgeGrabData LEDGE_GRAB_DATA;
        [Space(3)] public RagdollData RAGDOLL_DATA;
        [Space(3)] public ManualInputData MANUAL_INPUT_DATA;
        [Space(3)] public BoxColliderData BOX_COLLIDER_DATA;
        [Space(3)] public VerticalVelocityData VERTICAL_VELOCITY_DATA;
        [Space(3)] public TransitionData TRANSITION_DATA;
        [Space(3)] public AbilityData ABILITY_DATA;
        [Space(3)] public SpawnedObjData SPAWNED_OBJ_DATA;
        [Space(3)] public RotationData ROTATION_DATA;
        [Space(3)] public JumpData JUMP_DATA;
        [Space(3)] public CollisionSphereData COLLISION_SPHERE_DATA;
        [Space(3)] public GroundData GROUND_DATA;
        [Space(3)] public AttackData ATTACK_DATA;
        [Space(3)] public CollidingObjData COLLIDING_OBJ_DATA;
        [Space(3)] public WeaponData WEAPON_DATA;
        [Space(3)] public TurnData TURN_DATA;
        [Space(3)] public CameraData CAMERA_DATA;

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

        public Animator ANIMATOR
        {
            get
            {
                if (SkinnedMeshAnimator == null)
                {
                    SkinnedMeshAnimator = this.gameObject.GetComponentInChildren<Animator>();
                }

                return SkinnedMeshAnimator;
            }
        }

        private Animator SkinnedMeshAnimator = null;

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

            characterUpdateProcessor.InitUpdaters();
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

        public bool GetBool(System.Type CharacterQueryType, HashClassKey key, int hashInt)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnBool(key, hashInt);
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