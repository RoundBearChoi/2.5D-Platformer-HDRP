using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_BlockedByWall : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            //foreach(KeyValuePair<GameObject, GameObject> data in control.BLOCKING_DATA.FrontBlockingObjs)
            //{
            //    foreach(GameObject obj in data.Value)
            //}

            for (int i = 0; i < control.COLLISION_SPHERE_DATA.FrontOverlapCheckers.Length; i++)
            {
                if (!control.COLLISION_SPHERE_DATA.FrontOverlapCheckers[i].ObjIsOverlapping)
                {
                    return false;
                }
            }

            return true;
        }
    }
}