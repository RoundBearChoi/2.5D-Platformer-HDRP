using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CurrentAbility : CharacterQuery
    {
        public override bool ReturnBool(System.Type abilityType)
        {
            if (!abilityType.IsSubclassOf(typeof(CharacterAbility)))
            {
                Debug.LogError(abilityType.ToString() + " is not a character ability");
            }

            foreach (KeyValuePair<CharacterAbility, int> data in
                control.DATASET.ABILITY_DATA.CurrentAbilities)
            {
                if (data.Key.GetType() == abilityType)
                {
                    return true;
                }
            }

            return false;
        }
    }
}