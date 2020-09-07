using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_DoubleTap_Up : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.GetUpdater(typeof(ManualInput)) == null)
            {
                return false;
            }

            if (!control.MANUAL_INPUT_DATA.DoubleTapUp())
            {
                return false;
            }

            return true;
        }
    }
}