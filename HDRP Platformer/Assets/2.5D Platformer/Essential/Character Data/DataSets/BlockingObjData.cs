using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class BlockingObjData
    {
        public Vector3 RaycastContact = new Vector3();

        public Dictionary<GameObject, List<GameObject>> FrontBlockingObjs =
            new Dictionary<GameObject, List<GameObject>>();

        public Dictionary<GameObject, List<GameObject>> DownBlockingObjs =
            new Dictionary<GameObject, List<GameObject>>();

        public Dictionary<GameObject, List<GameObject>> UpBlockingObjs =
            new Dictionary<GameObject, List<GameObject>>();

        public List<CharacterControl> MarioStompTargets =
            new List<CharacterControl>();

        public int FrontBlockingDicCount = 0;
        public int UpBlockingDicCount = 0;
    }
}