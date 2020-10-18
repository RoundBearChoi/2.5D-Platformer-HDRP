using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_BlockedByWall : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            return IsTrue(control);
        }

        public static bool IsTrue(CharacterControl control)
        {
            if (control.DATASET.BLOCKING_DATA.FrontBlockingObjs.Count !=
                control.DATASET.COLLISION_SPHERE_DATA.FrontSpheres.Length)
            {
                return false;
            }

            foreach (KeyValuePair<GameObject, List<GameObject>> data in
                control.DATASET.BLOCKING_DATA.FrontBlockingObjs)
            {
                if (data.Value == null)
                {
                    return false;
                }

                if (data.Value.Count == 0)
                {
                    return false;
                }

                foreach (GameObject obj in data.Value)
                {
                    bool isWall = false;

                    if (CharacterManager.Instance.GetCharacter(obj.transform.root.gameObject) == null)
                    {
                        if (!MeleeWeapon.IsWeapon(obj))
                        {
                            if (!TrapSpikes.IsTrap(obj))
                            {
                                isWall = true;
                            }
                        }
                    }

                    if (!isWall)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}