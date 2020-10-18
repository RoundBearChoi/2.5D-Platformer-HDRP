using System.Collections.Generic;
using UnityEngine;

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

        [Header("SubComponents")]
        public CharacterSetup characterSetup;
        public CharacterFunctionProcessor characterFunctionProcessor;
        public CharacterUpdateProcessor characterUpdateProcessor;
        public CharacterQueryProcessor characterQueryProcessor;

        public GameObject RIGHT_HAND_ATTACK => characterSetup.attackPartSetup.RightHand_Attack;
        public GameObject LEFT_HAND_ATTACK => characterSetup.attackPartSetup.LeftHand_Attack;
        public GameObject RIGHT_FOOT_ATTACK => characterSetup.attackPartSetup.RightFoot_Attack;
        public GameObject LEFT_FOOT_ATTACK => characterSetup.attackPartSetup.LeftFoot_Attack;

        public Datasets DATASET
        {
            get
            {
                if (characterDatasets == null)
                {
                    characterDatasets = this.gameObject.GetComponent<Datasets>();
                }

                return characterDatasets;
            }
        }

        private Datasets characterDatasets;

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

        public BoxCollider BOX_COLLIDER
        {
            get
            {
                if (rootBoxCollider == null)
                {
                    rootBoxCollider = this.gameObject.GetComponent<BoxCollider>();
                }

                return rootBoxCollider;
            }
        }

        private BoxCollider rootBoxCollider;

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
            DATASET.GROUND_DATA.BoxColliderContacts = collision.contacts;
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