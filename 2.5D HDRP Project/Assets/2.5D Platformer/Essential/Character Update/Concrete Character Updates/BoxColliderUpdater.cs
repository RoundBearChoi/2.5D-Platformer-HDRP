using System.Collections;
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
                control.COLLISION_SPHERE_DATA.Reposition_FrontSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_BottomSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_BackSpheres();
                control.COLLISION_SPHERE_DATA.Reposition_UpSpheres();

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

        public void UpdateBoxCollider_Size()
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(control.boxCollider.size - control.BOX_COLLIDER_DATA.TargetSize) > 0.00001f)
            {
                control.boxCollider.size = Vector3.Lerp(control.boxCollider.size,
                    control.BOX_COLLIDER_DATA.TargetSize,
                    Time.deltaTime * control.BOX_COLLIDER_DATA.Size_Update_Speed);

                control.BOX_COLLIDER_DATA.IsUpdatingSpheres = true;
            }
        }

        public void UpdateBoxCollider_Center()
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(UpdateBoxCollider)))
            {
                return;
            }

            if (Vector3.SqrMagnitude(control.boxCollider.center - control.BOX_COLLIDER_DATA.TargetCenter) > 0.00001f)
            {
                control.boxCollider.center = Vector3.Lerp(control.boxCollider.center,
                    control.BOX_COLLIDER_DATA.TargetCenter,
                    Time.deltaTime * control.BOX_COLLIDER_DATA.Center_Update_Speed);

                control.BOX_COLLIDER_DATA.IsUpdatingSpheres = true;
            }
        }
    }
}