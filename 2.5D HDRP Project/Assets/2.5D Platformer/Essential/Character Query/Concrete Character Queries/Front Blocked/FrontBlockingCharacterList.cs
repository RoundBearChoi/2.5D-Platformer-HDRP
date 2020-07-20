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

            foreach (KeyValuePair<GameObject, GameObject> data in control.BLOCKING_DATA.FrontBlockingObjs)
            {
                CharacterControl c = CharacterManager.Instance.GetCharacter(data.Value.transform.root.gameObject);

                if (c != null)
                {
                    if (!objList.Contains(c.gameObject))
                    {
                        objList.Add(c.gameObject);
                    }
                }
            }

            return objList;
        }
    }
}