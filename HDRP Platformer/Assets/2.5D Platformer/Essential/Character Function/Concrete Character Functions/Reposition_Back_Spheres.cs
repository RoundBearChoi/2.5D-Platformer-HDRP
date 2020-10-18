using UnityEngine;

namespace Roundbeargames
{
    public class Reposition_Back_Spheres : CharacterFunction
    {
        public override void RunFunction()
        {
            float bottom = control.BOX_COLLIDER.bounds.center.y -
                (control.BOX_COLLIDER.bounds.size.y / 2f);

            float top = control.BOX_COLLIDER.bounds.center.y +
                (control.BOX_COLLIDER.bounds.size.y / 2f);

            float back = control.BOX_COLLIDER.bounds.center.z -
                (control.BOX_COLLIDER.bounds.size.z / 2f);

            control.COLLISION_SPHERE_DATA.BackSpheres[0].transform.localPosition =
                new Vector3(0f, bottom + 0.05f, back) - control.transform.position;

            control.COLLISION_SPHERE_DATA.BackSpheres[1].transform.localPosition =
                new Vector3(0f, top, back) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < control.COLLISION_SPHERE_DATA.BackSpheres.Length; i++)
            {
                control.COLLISION_SPHERE_DATA.BackSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom + (interval * (i - 1)), back) - control.transform.position;
            }
        }
    }
}