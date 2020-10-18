using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BoxColliderUpdater : CharacterUpdate
    {
        BoxColliderData BOXCOL_DATA => control.DATASET.BOX_COLLIDER_DATA;

        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            BOXCOL_DATA.IsUpdatingSpheres = false;

            UpdateBoxCollider_Size();
            UpdateBoxCollider_Center();

            if (BOXCOL_DATA.IsUpdatingSpheres)
            {
                control.RunFunction(typeof(Reposition_Front_Spheres));
                control.RunFunction(typeof(Reposition_Bottom_Spheres));
                control.RunFunction(typeof(Reposition_Back_Spheres));
                control.RunFunction(typeof(Reposition_Up_Spheres));

                if (BOXCOL_DATA.IsLanding)
                {
                    Debug.Log("repositioning y");

                    control.RIGID_BODY.MovePosition(new Vector3(
                        0f,
                        BOXCOL_DATA.LandingPosition.y,
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
                control.BOX_COLLIDER.size - BOXCOL_DATA.TargetSize) > 0.00001f)
            {
                control.BOX_COLLIDER.size = Vector3.Lerp(control.BOX_COLLIDER.size,
                    BOXCOL_DATA.TargetSize,
                    Time.deltaTime * BOXCOL_DATA.Size_Update_Speed);

                BOXCOL_DATA.IsUpdatingSpheres = true;
            }
        }

        public void UpdateBoxCollider_Center()
        {
            if (!control.UpdatingAbility(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(control.BOX_COLLIDER.center - BOXCOL_DATA.TargetCenter) > 0.00001f)
            {
                control.BOX_COLLIDER.center = Vector3.Lerp(
                    control.BOX_COLLIDER.center,
                    BOXCOL_DATA.TargetCenter,
                    Time.deltaTime * BOXCOL_DATA.Center_Update_Speed);

                BOXCOL_DATA.IsUpdatingSpheres = true;
            }
        }
    }
}