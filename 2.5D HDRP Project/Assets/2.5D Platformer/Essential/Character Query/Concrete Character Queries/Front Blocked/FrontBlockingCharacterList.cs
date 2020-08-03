using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class FrontBlockingCharacterList : CharacterQuery
    {
        [SerializeField]
        List<GameObject> objList = new List<GameObject>();

        public override List<GameObject> ReturnGameObjList()
        {
            objList.Clear();

            foreach (KeyValuePair<GameObject, List<GameObject>> data in control.BLOCKING_DATA.FrontBlockingObjs)
            {
                foreach(GameObject obj in data.Value)
                {
                    CharacterControl c = CharacterManager.Instance.GetCharacter(obj.transform.root.gameObject);

                    if (c != null)
                    {
                        if (!objList.Contains(c.gameObject))
                        {
                            objList.Add(c.gameObject);
                        }
                    }
                }
            }

            return objList;
        }
    }
}