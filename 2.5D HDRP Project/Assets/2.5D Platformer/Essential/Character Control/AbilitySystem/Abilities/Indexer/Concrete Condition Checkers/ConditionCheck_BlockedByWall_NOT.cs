using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_BlockedByWall_NOT : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            bool AllIsOverlapping = true;

            for (int i = 0; i < control.COLLISION_SPHERE_DATA.FrontOverlapCheckers.Length; i++)
            {
                if (!control.COLLISION_SPHERE_DATA.FrontOverlapCheckers[i].ObjIsOverlapping)
                {
                    AllIsOverlapping = false;
                }
            }

            if (AllIsOverlapping)
            {
                return false;
            }

            return true;
        }
    }
}