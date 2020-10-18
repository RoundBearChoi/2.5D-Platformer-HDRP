using UnityEngine;

namespace Roundbeargames
{
    public class Reposition_Up_Spheres : CharacterFunction
    {
        CollisionSphereData COLLISION_SPHERES => control.DATASET.COLLISION_SPHERE_DATA;

        public override void RunFunction()
        {
            float top = control.BOX_COLLIDER.bounds.center.y + (control.BOX_COLLIDER.bounds.size.y / 2f);
            float front = control.BOX_COLLIDER.bounds.center.z + (control.BOX_COLLIDER.bounds.size.z / 2f);
            float back = control.BOX_COLLIDER.bounds.center.z - (control.BOX_COLLIDER.bounds.size.z / 2f);

            COLLISION_SPHERES.UpSpheres[0].transform.localPosition =
                new Vector3(0f, top, back) - control.transform.position;

            COLLISION_SPHERES.UpSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < COLLISION_SPHERES.UpSpheres.Length; i++)
            {
                COLLISION_SPHERES.UpSpheres[i].transform.localPosition =
                    new Vector3(0f, top, back + (interval * (i - 1))) - control.transform.position;
            }
        }
    }
}