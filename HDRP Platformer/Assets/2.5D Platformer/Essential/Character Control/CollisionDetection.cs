using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CollisionDetection : MonoBehaviour
    {
        public static bool IgnoreCollision(CharacterControl control, RaycastHit hit)
        {
            if (IsBodyPart(control, hit.collider) ||
                    IsIgnoringCharacter(control, hit.collider) ||
                    Ledge.IsLedgeChecker(hit.collider.gameObject) ||
                    MeleeWeapon.IsWeapon(hit.collider.gameObject) ||
                    TrapSpikes.IsTrap(hit.collider.gameObject))
            {
                return true;
            }

            return false;
        }

        static bool IsIgnoringCharacter(CharacterControl control, Collider col)
        {
            if (!control.DATASET.MOVE_DATA.IsIgnoreCharacterTime)
            {
                return false;
            }
            else
            {
                CharacterControl blockingChar = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

                if (blockingChar == null)
                {
                    return false;
                }

                if (blockingChar == control)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        static bool IsBodyPart(CharacterControl control, Collider col)
        {
            if (col.transform.root.gameObject == control.gameObject)
            {
                return true;
            }

            CharacterControl target = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

            if (target == null)
            {
                return false;
            }

            if (target.GetBool(typeof(CharacterDead)))
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