using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterDead : CharacterQuery
    {
        public override bool ReturnBool()
        {
            if (control.DATASET.DAMAGE_DATA.hp <= 0f)
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