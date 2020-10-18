using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterQueryList : ScriptableObject
    {
        public List<System.Type> QueryTypes = new List<System.Type>();

        public virtual List<System.Type> GetList()
        {
            throw new System.NotImplementedException();
        }
    }
}