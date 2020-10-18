using System.Collections.Generic;

namespace Roundbeargames
{
    [System.Serializable]
    public class AbilityData
    {
        public Dictionary<CharacterAbility, int> CurrentAbilities =
            new Dictionary<CharacterAbility, int>();
    }
}