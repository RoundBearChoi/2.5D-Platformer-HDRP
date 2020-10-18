using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterUpdateProcessor : MonoBehaviour
    {
        public Dictionary<System.Type, CharacterUpdate> DicUpdaters = new Dictionary<System.Type, CharacterUpdate>();
        
        public CharacterControl control
        {
            get
            {
                if (characterControl == null)
                {
                    characterControl = this.gameObject.GetComponentInParent<CharacterControl>();
                }

                return characterControl;
            }
        }

        private CharacterControl characterControl;

        public void InitUpdaters()
        {
            AddUpdater(typeof(LedgeChecker));
            AddUpdater(typeof(DamageDetector));
            AddUpdater(typeof(Ragdoll));
            AddUpdater(typeof(BlockingObj));
            AddUpdater(typeof(BoxColliderUpdater));
            AddUpdater(typeof(VerticalVelocity));
            AddUpdater(typeof(PlayerRotation));
            AddUpdater(typeof(PlayerJump));
            AddUpdater(typeof(PlayerGround));
            AddUpdater(typeof(PlayerAttack));
            AddUpdater(typeof(PlayerAnimation));
            AddUpdater(typeof(CollisionSpheres));

            if (control.characterSetup.playableCharacterType != PlayableCharacterType.NONE)
            {
                AddUpdater(typeof(ManualInput));
            }
        }

        void AddUpdater(System.Type UpdaterType)
        {
            if (UpdaterType.IsSubclassOf(typeof(CharacterUpdate)))
            {
                _AddUpdater(UpdaterType);
            }
        }

        void _AddUpdater(System.Type UpdaterType)
        {
            GameObject obj = new GameObject();
            obj.name = UpdaterType.ToString();
            obj.name = obj.name = obj.name.Replace("Roundbeargames.", "");
            obj.transform.parent = this.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            CharacterUpdate u = obj.AddComponent(UpdaterType) as CharacterUpdate;
            DicUpdaters.Add(UpdaterType, u);

            u.InitComponent();
        }

        public void RunCharacterFixedUpdate()
        {
            CharacterFixedUpdate(typeof(LedgeChecker));
            CharacterFixedUpdate(typeof(Ragdoll));
            CharacterFixedUpdate(typeof(BlockingObj));
            CharacterFixedUpdate(typeof(BoxColliderUpdater));
            CharacterFixedUpdate(typeof(VerticalVelocity));
            CharacterFixedUpdate(typeof(DamageDetector));
            CharacterFixedUpdate(typeof(PlayerRotation));
        }

        public void RunCharacterUpdate()
        {
            CharacterUpdate(typeof(ManualInput));
            CharacterUpdate(typeof(PlayerAttack));
            CharacterUpdate(typeof(PlayerAnimation));
        }

        public void RunCharacterLateUpdate()
        {
            CharacterLateUpdate(typeof(BlockingObj));
        }

        void CharacterUpdate(System.Type UpdaterType)
        {
            if (control.characterUpdateProcessor.DicUpdaters.ContainsKey(UpdaterType))
            {
                control.characterUpdateProcessor.DicUpdaters[UpdaterType].OnUpdate();
            }
        }

        void CharacterFixedUpdate(System.Type UpdaterType)
        {
            if (control.characterUpdateProcessor.DicUpdaters.ContainsKey(UpdaterType))
            {
                control.characterUpdateProcessor.DicUpdaters[UpdaterType].OnFixedUpdate();
            }
        }

        void CharacterLateUpdate(System.Type UpdaterType)
        {
            if (control.characterUpdateProcessor.DicUpdaters.ContainsKey(UpdaterType))
            {
                control.characterUpdateProcessor.DicUpdaters[UpdaterType].OnLateUpdate();
            }
        }
    }
}
