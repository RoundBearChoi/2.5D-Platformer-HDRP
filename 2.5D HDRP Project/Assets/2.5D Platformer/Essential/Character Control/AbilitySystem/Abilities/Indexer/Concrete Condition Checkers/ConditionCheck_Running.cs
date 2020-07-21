using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Running : CheckCondition
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (!control.Turbo)
            {
                return false;
            }

            if (control.MoveLeft && control.MoveRight)
            {
                return false;
            }

            if (!control.MoveLeft && !control.MoveRight)
            {
                return false;
            }

            return true;
        }
    }
}