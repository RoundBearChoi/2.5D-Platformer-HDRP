using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class BlockingObjData
    {
        public Vector3 RaycastContact = new Vector3();

        public Dictionary<GameObject, GameObject> FrontBlockingObjs;

        public int FrontBlockingDicCount;
        public int UpBlockingDicCount;

        public delegate void DoSomething();
        public delegate bool ReturnBool();
        public delegate List<GameObject> ReturnGameObjList();

        public ReturnBool RightSideBlocked;
        public ReturnGameObjList GetFrontBlockingObjList;
        public ReturnGameObjList GetFrontBlockingCharacterList;
    }
}