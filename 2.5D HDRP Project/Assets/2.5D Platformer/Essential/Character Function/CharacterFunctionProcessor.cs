using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterFunctionProcessor : MonoBehaviour
    {
        public Dictionary<System.Type, CharacterFunction> DicFunctions = new Dictionary<System.Type, CharacterFunction>();

        private void Start()
        {
            AddFunction(typeof(LedgeCollidersOff));
            AddFunction(typeof(ClearRagdollVelocity));
            AddFunction(typeof(ClearUpVelocity));
            AddFunction(typeof(CheckDownBlocking));
            AddFunction(typeof(CheckUpBlocking));
            AddFunction(typeof(CheckMarioStomp));

            AddFunction(typeof(SpawnHitParticles));
            AddFunction(typeof(AddForceToDamagedPart));
            AddFunction(typeof(MoveTransformForward));
            AddFunction(typeof(CalculateMomentum));
            AddFunction(typeof(FaceForward));
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