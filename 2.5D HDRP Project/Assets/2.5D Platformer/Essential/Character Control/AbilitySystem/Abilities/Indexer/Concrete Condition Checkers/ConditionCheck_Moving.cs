using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Moving : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.MoveLeft || control.MoveRight)
            {
                if (!(control.MoveLeft && control.MoveRight))
                {
                    return true;
                }
            }

            return false;
        }
    }
}