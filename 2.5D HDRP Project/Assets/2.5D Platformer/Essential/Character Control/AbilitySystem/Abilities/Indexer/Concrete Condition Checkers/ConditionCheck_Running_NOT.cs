using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Running_NOT : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.Turbo)
            {
                if (control.MoveLeft || control.MoveRight)
                {
                    if (!(control.MoveLeft && control.MoveRight))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}