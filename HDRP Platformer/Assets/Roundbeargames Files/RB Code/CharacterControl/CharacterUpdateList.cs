using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterUpdateList : ScriptableObject
    {
        public List<System.Type> UpdateTypes = new List<System.Type>();

        public virtual List<System.Type> GetList()
        {
            throw new System.NotImplementedException();
        }
    }
}