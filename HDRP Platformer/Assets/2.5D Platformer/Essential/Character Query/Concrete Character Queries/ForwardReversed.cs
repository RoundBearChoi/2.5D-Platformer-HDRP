using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ForwardReversed : CharacterQuery
    {
        public override bool ReturnBool()
        {
            if (control.MOVE_DATA.LatestMoveForward.IsMoveOnHit())
            {
                if (control.GetBool(typeof(FacingAttacker)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (control.MOVE_DATA.LatestMoveForward.GetMoveSpeed() > 0f)
            {
                return false;
            }
            else if (control.MOVE_DATA.LatestMoveForward.GetMoveSpeed() < 0f)
            {
                return true;
            }

            return false;
        }
    }
}