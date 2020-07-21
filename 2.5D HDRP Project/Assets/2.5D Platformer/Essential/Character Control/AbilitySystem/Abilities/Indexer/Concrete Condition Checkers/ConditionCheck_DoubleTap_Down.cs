using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_DoubleTap_Down : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.GetUpdater(typeof(ManualInput)) == null)
            {
                return false;
            }

            if (!control.MANUAL_INPUT_DATA.DoubleTapDown())
            {
                return false;
            }

            return true;
        }
    }
}