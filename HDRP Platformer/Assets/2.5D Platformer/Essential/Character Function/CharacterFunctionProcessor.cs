using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterFunctionProcessor : MonoBehaviour
    {
        public Dictionary<System.Type, CharacterFunction> DicFunctions = new Dictionary<System.Type, CharacterFunction>();
        public CharacterFunctionList FunctionListType;

        private void Awake()
        {
            if (FunctionListType != null)
            {
                Debug.Log("Loading Character Functions: " +
                    this.name +" - " + this.transform.root.gameObject.name);

                List<System.Type> functions = FunctionListType.GetList();

                foreach(System.Type t in functions)
                {
                    AddFunction(t);
                }
            }
            else
            {
                Debug.Log("Loading Default Character Functions: " + this.transform.root.gameObject.name);
                SetDefaultFunctions();
            }

            CharacterControl control = this.gameObject.GetComponentInParent<CharacterControl>();
            control.InitalizeCharacter();
        }

        void SetDefaultFunctions()
        {
            AddFunction(typeof(LedgeCollidersOff));
            AddFunction(typeof(ClearRagdollVelocity));
            AddFunction(typeof(ClearUpVelocity));
            AddFunction(typeof(CheckDownBlocking));
            AddFunction(typeof(CheckUpBlocking));
            AddFunction(typeof(CheckMarioStomp));
            AddFunction(typeof(Reposition_Front_Spheres));
            AddFunction(typeof(Reposition_Back_Spheres));
            AddFunction(typeof(Reposition_Bottom_Spheres));
            AddFunction(typeof(Reposition_Up_Spheres));

            AddFunction(typeof(SpawnHitParticles));
            AddFunction(typeof(AddForceToDamagedPart));
            AddFunction(typeof(MoveTransformForward));
            AddFunction(typeof(CalculateMomentum));
            AddFunction(typeof(FaceForward));
            AddFunction(typeof(DoLedgeGrab));
            AddFunction(typeof(TurnIntoFlyingRagdoll));
            AddFunction(typeof(GetPushedAsRagdoll));
            AddFunction(typeof(TakeDamage));
            AddFunction(typeof(DamageReaction));
            AddFunction(typeof(TakeDamageFromThrownWeapon));
            AddFunction(typeof(ProcessMeleeWeaponContact));
            AddFunction(typeof(ProcessMeleeWeaponExit));
            AddFunction(typeof(InitCharacter));

            // AddFunction(typeof(ProcessDeathByInstaKill));
        }

        void AddFunction(System.Type type)
        {
            if (type.IsSubclassOf(typeof(CharacterFunction)))
            {
                GameObject obj = new GameObject();
                obj.transform.parent = this.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;

                CharacterFunction f = obj.AddComponent(type) as CharacterFunction;
                DicFunctions.Add(type, f);

                f.control = this.transform.root.gameObject.GetComponent<CharacterControl>();

                obj.name = type.ToString();
                obj.name = obj.name.Replace("Roundbeargames.", "");
            }
        }
    }
}