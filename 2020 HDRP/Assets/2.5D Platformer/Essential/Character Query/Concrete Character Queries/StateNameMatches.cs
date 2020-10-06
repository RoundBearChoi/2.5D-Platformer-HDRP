using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class StateNameMatches : CharacterQuery
    {
        public override bool ReturnBool(HashClassKey key, int hashInt)
        {
            if (HashManager.Instance.HASH_INITIALIZER.DicHashes.ContainsKey(key))
            {
                if (HashManager.Instance.HASH_INITIALIZER.DicHashes[key].Equals(hashInt))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}