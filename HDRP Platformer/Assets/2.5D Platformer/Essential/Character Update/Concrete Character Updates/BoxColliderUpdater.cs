﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BoxColliderUpdater : CharacterUpdate
    {
        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            control.BOX_COLLIDER_DATA.IsUpdatingSpheres = false;

            UpdateBoxCollider_Size();
            UpdateBoxCollider_Center();

            if (control.BOX_COLLIDER_DATA.IsUpdatingSpheres)
            {
                control.RunFunction(typeof(Reposition_Front_Spheres));
                control.RunFunction(typeof(Reposition_Bottom_Spheres));
                control.RunFunction(typeof(Reposition_Back_Spheres));
                control.RunFunction(typeof(Reposition_Up_Spheres));

                if (control.BOX_COLLIDER_DATA.IsLanding)
                {
                    Debug.Log("repositioning y");

                    control.RIGID_BODY.MovePosition(new Vector3(
                        0f,
                        control.BOX_COLLIDER_DATA.LandingPosition.y,
                        this.transform.position.z));

                    control.RIGID_BODY.velocity = Vector3.zero;
                }
            }
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBoxCollider_Size()
        {
            if (!control.UpdatingAbility(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(
                control.BOX_COLLIDER.size - control.BOX_COLLIDER_DATA.TargetSize) >
                0.00001f)
            {
                control.BOX_COLLIDER.size = Vector3.Lerp(control.BOX_COLLIDER.size,
                    control.BOX_COLLIDER_DATA.TargetSize,
                    Time.deltaTime * control.BOX_COLLIDER_DATA.Size_Update_Speed);

                control.BOX_COLLIDER_DATA.IsUpdatingSpheres = true;
            }
        }

        public void UpdateBoxCollider_Center()
        {
            if (!control.UpdatingAbility(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(
                control.BOX_COLLIDER.center -
                control.BOX_COLLIDER_DATA.TargetCenter) > 0.00001f)
            {
                control.BOX_COLLIDER.center = Vector3.Lerp(
                    control.BOX_COLLIDER.center,
                    control.BOX_COLLIDER_DATA.TargetCenter,
                    Time.deltaTime * control.BOX_COLLIDER_DATA.Center_Update_Speed);

                control.BOX_COLLIDER_DATA.IsUpdatingSpheres = true;
            }
        }
    }
}