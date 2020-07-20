using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class FrontBlockingObjList : CharacterQuery
    {
        [SerializeField]
        List<GameObject> ObjList = new List<GameObject>();

        public override List<GameObject> ReturnGameObjList()
        {
            ObjList.Clear();

            foreach (KeyValuePair<GameObject, GameObject> data in control.BLOCKING_DATA.FrontBlockingObjs)
            {
                if (!ObjList.Contains(data.Value))
                {
                    ObjList.Add(data.Value);
                }
            }

            return ObjList;
        }
    }
}