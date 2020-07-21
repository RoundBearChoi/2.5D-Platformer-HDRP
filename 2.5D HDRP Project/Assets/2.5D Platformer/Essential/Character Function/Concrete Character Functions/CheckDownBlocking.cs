using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CheckDownBlocking : CharacterFunction
    {
        public override void RunFunction(float RayDistance)
        {
            control.BLOCKING_DATA.DownBlockingObjs.Clear();

            foreach (GameObject obj in control.COLLISION_SPHERE_DATA.BottomSpheres)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(obj.transform.position, Vector3.down, RayDistance);

                foreach (RaycastHit h in hits)
                {
                    if (!CollisionDetection.IgnoreCollision(control, h))
                    {
                        AddObjToDictionary.Add(control.BLOCKING_DATA.DownBlockingObjs,
                            obj,
                            h.collider.transform.root.gameObject);
                    }
                }
            }
        }
    }
}