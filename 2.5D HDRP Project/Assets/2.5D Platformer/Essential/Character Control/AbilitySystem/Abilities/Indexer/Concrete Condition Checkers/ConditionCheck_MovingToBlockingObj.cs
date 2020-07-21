using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_MovingToBlockingObj : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            foreach (GameObject o in control.GetGameObjList(typeof(FrontBlockingObjList)))
            {
                Vector3 dir = o.transform.position - control.transform.position;

                if (dir.z > 0f && !control.MoveRight)
                {
                    return false;
                }

                if (dir.z < 0f && !control.MoveLeft)
                {
                    return false;
                }
            }

            return true;
        }
    }
}