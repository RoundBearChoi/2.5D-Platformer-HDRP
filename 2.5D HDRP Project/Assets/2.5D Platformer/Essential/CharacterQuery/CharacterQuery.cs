using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public abstract class CharacterQuery : MonoBehaviour
    {
        public CharacterControl control;
        
        public virtual bool ReturnBool()
        {
            throw new System.NotImplementedException();
        }

        public virtual List<GameObject> ReturnGameObjList()
        {
            throw new System.NotImplementedException();
        }

        public virtual GameObject ReturnGameObj(AttackPartType attackPartType)
        {
            throw new System.NotImplementedException();
        }
    }
}