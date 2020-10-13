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

            foreach (KeyValuePair<GameObject, List<GameObject>> data in control.BLOCKING_DATA.FrontBlockingObjs)
            {
                foreach(GameObject obj in data.Value)
                {
                    if (!ObjList.Contains(obj))
                    {
                        ObjList.Add(obj);
                    }
                }
            }

            return ObjList;
        }
    }
}