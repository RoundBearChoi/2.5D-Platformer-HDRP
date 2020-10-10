using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New Key", menuName = "Roundbeargames/AnimatorStateKey/AnimatorStateKey")]
    public class HashClassKey : ScriptableObject
    {
        public int ShortNameHash;
        public MirrorParameterType MirrorType;
    }
}