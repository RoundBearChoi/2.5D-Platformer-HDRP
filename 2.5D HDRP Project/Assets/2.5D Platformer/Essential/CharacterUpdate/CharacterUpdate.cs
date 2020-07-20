using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public abstract class CharacterUpdate : MonoBehaviour
    {
        protected CharacterUpdateProcessor characterUpdateProcessor;

        public CharacterControl control
        {
            get
            {
                return characterUpdateProcessor.control;
            }
        }

        private void Awake()
        {
            characterUpdateProcessor = this.gameObject.GetComponentInParent<CharacterUpdateProcessor>();
        }

        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();

        public virtual void InitComponent()
        {

        }
    }
}