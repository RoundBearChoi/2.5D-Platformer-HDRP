using UnityEngine;

namespace Roundbeargames
{
    public class Reposition_Front_Spheres : CharacterFunction
    {
        CollisionSphereData COLLISION_SPHERES => control.DATASET.COLLISION_SPHERE_DATA;

        public override void RunFunction()
        {
            float bottom = control.BOX_COLLIDER.bounds.center.y -
                (control.BOX_COLLIDER.bounds.size.y / 2f);

            float top = control.BOX_COLLIDER.bounds.center.y +
                (control.BOX_COLLIDER.bounds.size.y / 2f);

            float front = control.BOX_COLLIDER.bounds.center.z +
                (control.BOX_COLLIDER.bounds.size.z / 2f);

            COLLISION_SPHERES.FrontSpheres[0].transform.localPosition =
                new Vector3(0f, bottom + 0.05f, front) - control.transform.position;

            COLLISION_SPHERES.FrontSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < COLLISION_SPHERES.FrontSpheres.Length; i++)
            {
                COLLISION_SPHERES.FrontSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom + (interval * (i - 1)), front) - control.transform.position;
            }
        }
    }
}