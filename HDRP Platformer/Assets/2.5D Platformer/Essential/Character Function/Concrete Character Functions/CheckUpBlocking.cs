using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CheckUpBlocking : CharacterFunction
    {
        public override void RunFunction(float RayDistance)
        {
            control.DATASET.BLOCKING_DATA.UpBlockingObjs.Clear();

            foreach (GameObject obj in
                control.DATASET.COLLISION_SPHERE_DATA.UpSpheres)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(obj.transform.position, Vector3.up, RayDistance);

                foreach (RaycastHit h in hits)
                {
                    if (!CollisionDetection.IgnoreCollision(control, h))
                    {
                        AddObjToDictionary.Add(
                            control.DATASET.BLOCKING_DATA.UpBlockingObjs,
                            obj,
                            h.collider.transform.root.gameObject);
                    }
                }
            }
        }
    }
}