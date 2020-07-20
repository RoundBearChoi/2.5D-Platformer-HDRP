using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterData : MonoBehaviour
    {
        [Space(15)] public BlockingObjData blockingData;
        [Space(15)] public LedgeGrabData ledgeGrabData;
    }
}