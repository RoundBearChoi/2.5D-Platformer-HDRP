using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_BlockedByWall : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
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