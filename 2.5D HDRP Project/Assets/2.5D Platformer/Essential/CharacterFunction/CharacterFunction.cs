using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public abstract class CharacterFunction : MonoBehaviour
    {
        public CharacterControl control;

        public virtual void RunFunction()
        {
            throw new System.NotImplementedException();
        }
    }
}