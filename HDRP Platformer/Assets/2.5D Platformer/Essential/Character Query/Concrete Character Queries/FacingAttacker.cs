using UnityEngine;

namespace Roundbeargames
{
    public class FacingAttacker : CharacterQuery
    {
        DamageData DAMAGE => control.DATASET.DAMAGE_DATA;

        public override bool ReturnBool()
        {
            Vector3 vec = DAMAGE.damageTaken.ATTACKER.transform.position -
                control.transform.position;

            if (vec.z < 0f)
            {
                if (control.GetBool(typeof(FacingForward)))
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
                if (control.GetBool(typeof(FacingForward)))
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