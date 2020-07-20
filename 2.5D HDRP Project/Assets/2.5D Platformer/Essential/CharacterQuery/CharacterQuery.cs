using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public abstract class CharacterQuery : MonoBehaviour
    {
        protected CharacterControl control;

        private void Start()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();
        }

        public virtual bool ReturnBool()
        {
            throw new System.NotImplementedException();
        }
    }
}