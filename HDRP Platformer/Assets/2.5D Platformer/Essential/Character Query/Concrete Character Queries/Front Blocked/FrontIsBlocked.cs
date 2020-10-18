using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class FrontIsBlocked : CharacterQuery
    {
        public override bool ReturnBool()
        {
            if (control.DATASET.BLOCKING_DATA.FrontBlockingDicCount != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}