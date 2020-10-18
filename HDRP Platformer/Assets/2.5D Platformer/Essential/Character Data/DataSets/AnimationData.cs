using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationData
    {
        public Dictionary<CharacterAbility, int> CurrentRunningAbilities = new Dictionary<CharacterAbility, int>();
        public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();
    }
}