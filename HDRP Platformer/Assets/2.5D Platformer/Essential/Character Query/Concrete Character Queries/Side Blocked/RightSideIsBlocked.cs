using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class RightSideIsBlocked : CharacterQuery
    {
        public override bool ReturnBool()
        {
            foreach (KeyValuePair<GameObject, List<GameObject>> data in
                control.DATASET.BLOCKING_DATA.FrontBlockingObjs)
            {
                foreach(GameObject obj in data.Value)
                {
                    if ((obj.transform.position - control.transform.position).z > 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}