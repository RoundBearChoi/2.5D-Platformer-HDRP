using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LeftSideIsBlocked : CharacterQuery
    {
        public override bool ReturnBool()
        {
            foreach (KeyValuePair<GameObject, GameObject> data in control.BLOCKING_DATA.FrontBlockingObjs)
            {
                if ((data.Value.transform.position - control.transform.position).z < 0f)
                {
                    return true;
                }
            }

            return false;
        }
    }
}