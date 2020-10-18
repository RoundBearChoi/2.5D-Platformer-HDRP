﻿using UnityEngine;

namespace Roundbeargames
{
    public class Reposition_Up_Spheres : CharacterFunction
    {
        public override void RunFunction()
        {
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

            control.COLLISION_SPHERE_DATA.UpSpheres[0].transform.localPosition =
                new Vector3(0f, top, back) - control.transform.position;

            control.COLLISION_SPHERE_DATA.UpSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < control.COLLISION_SPHERE_DATA.UpSpheres.Length; i++)
            {
                control.COLLISION_SPHERE_DATA.UpSpheres[i].transform.localPosition =
                    new Vector3(0f, top, back + (interval * (i - 1))) - control.transform.position;
            }
        }
    }
}