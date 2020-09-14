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
            if (Front == null)
            {
                SetParents();
            }

            SetColliderSpheres();
        }

        public override void OnFixedUpdate()
        {

        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnLateUpdate()
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

            control.RunFunction(typeof(Reposition_Bottom_Spheres));

            // top

            for (int i = 0; i < 5; i++)
            {
                GameObject obj = LoadCollisionSphere();

                control.COLLISION_SPHERE_DATA.UpSpheres[i] = obj;
                obj.transform.parent = Up.transform;
            }

            control.RunFunction(typeof(Reposition_Up_Spheres));

            // front

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();

                control.COLLISION_SPHERE_DATA.FrontSpheres[i] = obj;
                obj.transform.parent = Front.transform;
            }

            control.RunFunction(typeof(Reposition_Front_Spheres));

            // back

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = LoadCollisionSphere();

                control.COLLISION_SPHERE_DATA.BackSpheres[i] = obj;
                obj.transform.parent = Back.transform;
            }

            control.RunFunction(typeof(Reposition_Back_Spheres));
        }
    }
}