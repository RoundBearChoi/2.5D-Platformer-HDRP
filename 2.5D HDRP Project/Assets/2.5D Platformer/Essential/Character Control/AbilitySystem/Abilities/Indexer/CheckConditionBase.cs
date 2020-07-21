using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public abstract class CheckConditionBase : MonoBehaviour
    {
        public abstract bool MeetsCondition(CharacterControl control);
    }
}