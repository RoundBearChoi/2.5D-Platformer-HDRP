using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class StateNameMatches : CharacterQuery
    {
        public override bool ReturnBool(HashClassKey key, int hashInt)
        {
            if (key.ShortNameHash.Equals(hashInt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}