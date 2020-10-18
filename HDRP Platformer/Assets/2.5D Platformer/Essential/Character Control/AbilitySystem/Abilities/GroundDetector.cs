using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/GroundDetector")]
    public class GroundDetector : CharacterAbility
    {
        public float Distance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (IsGrounded(characterState.control))
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded], true);
            }
            else
            {
                animator.SetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded], false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool IsGrounded(CharacterControl control)
        {
            // physics check
            if (control.DATASET.GROUND_DATA.BoxColliderContacts != null)
            {
                foreach (ContactPoint c in control.DATASET.GROUND_DATA.BoxColliderContacts)
                {
                    float colliderBottom = (
                        control.transform.position.y +
                        control.BOX_COLLIDER.center.y) -
                        (control.BOX_COLLIDER.size.y / 2f);

                    float yDifference = Mathf.Abs(c.point.y - colliderBottom);

                    if (yDifference < 0.01f)
                    {
                        if (Mathf.Abs(control.RIGID_BODY.velocity.y) < 0.001f)
                        {
                            control.DATASET.GROUND_DATA.Ground = c.otherCollider.gameObject;
                            SetLandingPosition(control, c.point);
                            return true;
                        }
                    }
                }
            }

            // raycast check
            if (control.RIGID_BODY.velocity.y < 0f)
            {
                foreach (GameObject o in
                    control.DATASET.COLLISION_SPHERE_DATA.BottomSpheres)
                {
                    RaycastHit[] hits = Physics.RaycastAll(o.transform.position, Vector3.down, Distance);

                    foreach(RaycastHit h in hits)
                    {
                        if (!CollisionDetection.IgnoreCollision(control, h))
                        {
                            CharacterControl c = CharacterManager.Instance.GetCharacter(h.transform.root.gameObject);

                            if (c == null)
                            {
                                control.DATASET.GROUND_DATA.Ground = h.transform.gameObject;
                                SetLandingPosition(control, h.point);
                                return true;
                            }
                        }
                    }
                }
            }

            control.DATASET.GROUND_DATA.Ground = null;
            return false;
        }

        void SetLandingPosition(CharacterControl control, Vector3 pos)
        {
            if (control.ANIMATOR.GetBool(
                HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]) == false)
            {
                _setlandingposition(control, pos);
            }
        }

        void _setlandingposition(CharacterControl control, Vector3 pos)
        {
            control.DATASET.BOX_COLLIDER_DATA.LandingPosition = new Vector3(
                0f,
                pos.y,
                pos.z);

            if (control.DATASET.BLOCKING_DATA.DownBlockingObjs.Count > 0 &&
                control.DATASET.BLOCKING_DATA.DownBlockingObjs.Count < 5)
            {
                foreach (KeyValuePair<GameObject, List<GameObject>> data in
                    control.DATASET.BLOCKING_DATA.DownBlockingObjs)
                {
                    if (data.Key.transform.position.z < control.transform.position.z)
                    {
                        control.transform.position -= Vector3.forward * 0.01f;
                        break;
                    }

                    if (data.Key.transform.position.z > control.transform.position.z)
                    {
                        control.transform.position += Vector3.forward * 0.01f;
                        break;
                    }
                }
            }
        }
    }
}