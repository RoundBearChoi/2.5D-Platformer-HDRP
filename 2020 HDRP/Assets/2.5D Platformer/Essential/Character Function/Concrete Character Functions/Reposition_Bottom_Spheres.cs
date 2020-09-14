using UnityEngine;

namespace Roundbeargames
{
    public class Reposition_Bottom_Spheres : CharacterFunction
    {
        public override void RunFunction()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            control.COLLISION_SPHERE_DATA.BottomSpheres[0].transform.localPosition =
                new Vector3(0f, bottom, back) - control.transform.position;

            control.COLLISION_SPHERE_DATA.BottomSpheres[1].transform.localPosition =
                new Vector3(0f, bottom, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < control.COLLISION_SPHERE_DATA.BottomSpheres.Length; i++)
            {
                control.COLLISION_SPHERE_DATA.BottomSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom, back + (interval * (i - 1))) - control.transform.position;
            }
        }
    }
}