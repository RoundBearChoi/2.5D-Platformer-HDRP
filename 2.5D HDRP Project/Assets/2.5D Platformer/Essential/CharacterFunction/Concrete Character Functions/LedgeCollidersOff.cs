using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeCollidersOff : CharacterFunction
    {
        public override void RunFunction()
        {
            control.LEDGE_GRAB_DATA.Collider1.SetActive(false);
            control.LEDGE_GRAB_DATA.Collider2.SetActive(false);
        }
    }
}