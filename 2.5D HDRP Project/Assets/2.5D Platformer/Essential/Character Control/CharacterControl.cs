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

        private void Awake()
        {
            characterUpdateProcessor = GetComponentInChildren<CharacterUpdateProcessor>();

            // temp
            aiProgress = GetComponentInChildren<AIProgress>();
            boxCollider = GetComponent<BoxCollider>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();

            aiController = GetComponentInChildren<AIController>();
            if (aiController == null)
            {
                if (navMeshObstacle != null)
                {
                    navMeshObstacle.carving = true;
                }
            }

            characterSetup = GetComponentInChildren<CharacterSetup>();
            characterData = GetComponentInChildren<CharacterData>();
            characterFunctionProcessor = GetComponentInChildren<CharacterFunctionProcessor>();
            characterQueryProcessor = GetComponentInChildren<CharacterQueryProcessor>();

            RegisterCharacter();
            InitCharacterStates(characterSetup.SkinnedMeshAnimator);
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

        void InitCharacterStates(Animator animator)
        {
            CharacterState[] arr = animator.GetBehaviours<CharacterState>();

            foreach(CharacterState c in arr)
            {
                c.characterControl = this;
            }
        }

        void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
        }

        public void RunFunction(System.Type CharacterFunctionType)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction();
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

        public bool GetBool(System.Type CharacterQueryType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnBool();
        }

        public bool GetBool(System.Type CharacterQueryType, string str)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnBool(str);
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