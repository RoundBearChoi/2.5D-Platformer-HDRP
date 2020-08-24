using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationData
    {
        public bool InstantTransitionMade;
        public MoveForwardComponent LatestMoveForward;
        public MoveUp LatestMoveUp;
        public bool LockTransition;
        public bool IsIgnoreCharacterTime;
        public Dictionary<CharacterAbility, int> CurrentRunningAbilities = new Dictionary<CharacterAbility, int>();
        public List<PoolObjectType> SpawnedObjList = new List<PoolObjectType>();

        public delegate bool bool_type(System.Type type);
        public bool_type IsRunning;
    }
}