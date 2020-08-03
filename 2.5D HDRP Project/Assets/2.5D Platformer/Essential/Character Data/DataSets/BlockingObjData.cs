using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class BlockingObjData
    {
        public Vector3 RaycastContact = new Vector3();

        public Dictionary<GameObject, List<GameObject>> FrontBlockingObjs;
        public Dictionary<GameObject, GameObject> DownBlockingObjs;
        public Dictionary<GameObject, GameObject> UpBlockingObjs;

        public int FrontBlockingDicCount;
        public int UpBlockingDicCount;
    }
}