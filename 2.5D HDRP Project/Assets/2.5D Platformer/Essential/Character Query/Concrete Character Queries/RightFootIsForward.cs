using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class RightFootIsForward : CharacterQuery
    {
        public override bool ReturnBool()
        {
            if (control.GetBool(typeof(FacingForward)))
            {
                if (RightFootHasHigherZ())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!RightFootHasHigherZ())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        bool RightFootHasHigherZ()
        {
            if (control.characterSetup.attackPartSetup.RightFoot_Attack.transform.position.z >
                control.characterSetup.attackPartSetup.LeftFoot_Attack.transform.position.z)
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