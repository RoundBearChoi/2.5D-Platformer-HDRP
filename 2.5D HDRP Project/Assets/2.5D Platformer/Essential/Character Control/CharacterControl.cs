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

        [Header("SubComponents")]
        public CharacterUpdateProcessor characterUpdateProcessor;

        // temp
        public AnimationProgress animationProgress;
        public AIProgress aiProgress;
        public AIController aiController;
        public BoxCollider boxCollider;
        public NavMeshObstacle navMeshObstacle;

        public CharacterData characterData;
        public CharacterFunctionProcessor characterFunctionProcessor;
        public CharacterQueryProcessor characterQueryProcessor;

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

        [Header("Setup")]
        public PlayableCharacterType playableCharacterType;
        public Animator SkinnedMeshAnimator;
        public GameObject LeftHand_Attack;
        public GameObject RightHand_Attack;
        public GameObject LeftFoot_Attack;
        public GameObject RightFoot_Attack;
        
        private Dictionary<string, GameObject> ChildObjects = new Dictionary<string, GameObject>();

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

        private void Awake()
        {
            characterUpdateProcessor = GetComponentInChildren<CharacterUpdateProcessor>();

            // temp
            animationProgress = GetComponent<AnimationProgress>();
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

            characterData = GetComponentInChildren<CharacterData>();
            characterFunctionProcessor = GetComponentInChildren<CharacterFunctionProcessor>();
            characterQueryProcessor = GetComponentInChildren<CharacterQueryProcessor>();

            RegisterCharacter();
            InitCharacterStates(SkinnedMeshAnimator);
        }


        private void Update()
        {
            characterUpdateProcessor.RunCharacterUpdate();
        }

        private void FixedUpdate()
        {
            characterUpdateProcessor.RunCharacterFixedUpdate();
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
                //c.PutStatesInArray();
            }
        }

        void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
        }

        public void MoveForward(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }

        public GameObject GetChildObj(string name)
        {
            if (ChildObjects.ContainsKey(name))
            {
                return ChildObjects[name];
            }

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach(Transform t in arr)
            {
                if (t.gameObject.name.Equals(name))
                {
                    ChildObjects.Add(name, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }

        public void RunFunction(System.Type CharacterFunctionType)
        {
            characterFunctionProcessor.DicFunctions[CharacterFunctionType].RunFunction();
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

        public List<GameObject> GetGameObjList(System.Type CharacterQueryType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnGameObjList();
        }

        public GameObject GetGameObject(System.Type CharacterQueryType, AttackPartType attackPartType)
        {
            return characterQueryProcessor.DicQueries[CharacterQueryType].ReturnGameObj(attackPartType);
        }
    }
}