using UnityEngine;

namespace Roundbeargames
{
    public class FacingAttacker : CharacterQuery
    {
        public override bool ReturnBool()
        {
            Vector3 vec = control.DAMAGE_DATA.damageTaken.ATTACKER.transform.position -
                control.transform.position;

            if (vec.z < 0f)
            {
                if (control.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (vec.z > 0f)
            {
                if (control.GetBool(typeof(FacingForward)))// ROTATION_DATA.IsFacingForward())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}