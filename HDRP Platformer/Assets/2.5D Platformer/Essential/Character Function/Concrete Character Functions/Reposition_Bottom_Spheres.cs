using UnityEngine;

namespace Roundbeargames
{
    public class Reposition_Bottom_Spheres : CharacterFunction
    {
        CollisionSphereData COLLISION_SPHERES => control.DATASET.COLLISION_SPHERE_DATA;

        public override void RunFunction()
        {
            float bottom = control.BOX_COLLIDER.bounds.center.y -
                (control.BOX_COLLIDER.bounds.size.y / 2f);

            float front = control.BOX_COLLIDER.bounds.center.z +
                (control.BOX_COLLIDER.bounds.size.z / 2f);

            float back = control.BOX_COLLIDER.bounds.center.z -
                (control.BOX_COLLIDER.bounds.size.z / 2f);

            COLLISION_SPHERES.BottomSpheres[0].transform.localPosition =
                new Vector3(0f, bottom, back) - control.transform.position;

            COLLISION_SPHERES.BottomSpheres[1].transform.localPosition =
                new Vector3(0f, bottom, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < COLLISION_SPHERES.BottomSpheres.Length; i++)
            {
                COLLISION_SPHERES.BottomSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom, back + (interval * (i - 1))) - control.transform.position;
            }
        }
    }
}