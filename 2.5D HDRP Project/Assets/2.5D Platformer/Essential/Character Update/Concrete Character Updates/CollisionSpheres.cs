using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CollisionSpheres : CharacterUpdate
    {
        GameObject Front = null;
        GameObject Back = null;
        GameObject Bottom = null;
        GameObject Up = null;

        public override void InitComponent()
        {
            control.COLLISION_SPHERE_DATA.FrontOverlapCheckerContains = FrontOverlapCheckerContains;
            control.COLLISION_SPHERE_DATA.Reposition_FrontSpheres = Reposition_FrontSpheres;
            control.COLLISION_SPHERE_DATA.Reposition_BottomSpheres = Reposition_BottomSpheres;
            control.COLLISION_SPHERE_DATA.Reposition_BackSpheres = Reposition_BackSpheres;
            control.COLLISION_SPHERE_DATA.Reposition_UpSpheres = Reposition_UpSpheres;

            characterUpdateProcessor.ArrCharacterUpdate[(int)CharacterUpdateType.COLLISION_SPHERES] = this;

            if (Front == null)
            {
                SetParents();
            }

            SetColliderSpheres();
        }

        public override void OnFixedUpdate()
        {
            for (int i = 0; i < control.COLLISION_SPHERE_DATA.AllOverlapCheckers.Length; i++)
            {
                control.COLLISION_SPHERE_DATA.AllOverlapCheckers[i].UpdateChecker();
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        GameObject LoadCollisionSphere()
        {
            return Instantiate(Resources.Load("CollisionSphere", typeof(GameObject)),
                    Vector3.zero, Quaternion.identity) as GameObject;
        }

        void SetParents()
        {
            CreateParentObj(ref Front, "Front");
            CreateParentObj(ref Back, "Back");
            CreateParentObj(ref Bottom, "Bottom");
            CreateParentObj(ref Up, "Up");
        }

        void CreateParentObj(ref GameObject obj, string name)
        {
            obj = new GameObject();
            obj.transform.parent = this.transform;
            obj.name = name;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }

        void SetColliderSpheres()
        {
            // bottom

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = LoadCollisionSphere();

                control.COLLISION_SPHERE_DATA.BottomSpheres[i] = obj;
                obj.transform.parent = Bottom.transform;
            }

            Reposition_BottomSpheres();

            // top

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = LoadCollisionSphere();

                control.COLLISION_SPHERE_DATA.UpSpheres[i] = obj;
                obj.transform.parent = Up.transform;
            }

            Reposition_UpSpheres();

            // front

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();

                control.COLLISION_SPHERE_DATA.FrontSpheres[i] = obj;
                control.COLLISION_SPHERE_DATA.FrontOverlapCheckers[i] = obj.GetComponent<OverlapChecker>();

                obj.transform.parent = Front.transform;
            }

            Reposition_FrontSpheres();

            // back

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();

                control.COLLISION_SPHERE_DATA.BackSpheres[i] = obj;
                obj.transform.parent = Back.transform;
            }

            Reposition_BackSpheres();

            // add everything

            OverlapChecker[] arr = this.gameObject.GetComponentsInChildren<OverlapChecker>();
            control.COLLISION_SPHERE_DATA.AllOverlapCheckers = arr;
        }

        void Reposition_FrontSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float front = control.boxCollider.bounds.center.z + (control.boxCollider.bounds.size.z / 2f);

            control.COLLISION_SPHERE_DATA.FrontSpheres[0].transform.localPosition =
                new Vector3(0f, bottom + 0.05f, front) - control.transform.position;

            control.COLLISION_SPHERE_DATA.FrontSpheres[1].transform.localPosition =
                new Vector3(0f, top, front) - control.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < control.COLLISION_SPHERE_DATA.FrontSpheres.Length; i++)
            {
                control.COLLISION_SPHERE_DATA.FrontSpheres[i].transform.localPosition =
                    new Vector3(0f, bottom + (interval * (i - 1)), front) - control.transform.position;
            }
        }

        void Reposition_BackSpheres()
        {
            float bottom = control.boxCollider.bounds.center.y - (control.boxCollider.bounds.size.y / 2f);
            float top = control.boxCollider.bounds.center.y + (control.boxCollider.bounds.size.y / 2f);
            float back = control.boxCollider.bounds.center.z - (control.boxCollider.bounds.size.z / 2f);

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

        void Reposition_BottomSpheres()
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

        void Reposition_UpSpheres()
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

        bool FrontOverlapCheckerContains(OverlapChecker checker)
        {
            for (int i = 0; i < control.COLLISION_SPHERE_DATA.FrontOverlapCheckers.Length; i++)
            {
                if (control.COLLISION_SPHERE_DATA.FrontOverlapCheckers[i] == checker)
                {
                    return true;
                }
            }

            return false;
        }
    }
}