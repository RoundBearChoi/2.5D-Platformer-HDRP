using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ForwardReversed : CharacterQuery
    {
        MoveData MOVE => control.DATASET.MOVE_DATA;

        public override bool ReturnBool()
        {
            if (MOVE.LatestMoveForward.IsMoveOnHit())
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

            if (MOVE.LatestMoveForward.GetMoveSpeed() > 0f)
            {
                return false;
            }
            else if (MOVE.LatestMoveForward.GetMoveSpeed() < 0f)
            {
                return true;
            }

            return false;
        }
    }
}