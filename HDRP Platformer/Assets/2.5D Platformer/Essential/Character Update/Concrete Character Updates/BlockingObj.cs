using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BlockingObj : CharacterUpdate
    {
        GameObject[] FrontSpheresArray;
        float FrontDirectionMultiplier;
        Vector3 FrontRayDirection = new Vector3();
        float FrontRayLength = 0f;

        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            if (control.UpdatingAbility(typeof(MoveForward)) ||
                control.UpdatingAbility(typeof(MoveForward_v2)) ||
                control.UpdatingAbility(typeof(WallSlide)))
            {
                CheckFrontBlocking();
            }

            // checking while ledge grabbing
            if (control.UpdatingAbility(typeof(MoveUp)))
            {
                if (control.MOVE_DATA.LatestMoveUp.Speed > 0f)
                {
                    control.RunFunction(typeof(CheckUpBlocking), 0.3f);
                }
            }
            else
            {
                // checking while jumping up
                if (control.RIGID_BODY.velocity.y > 0.001f)
                {
                    control.RunFunction(typeof(CheckUpBlocking), 0.125f);

                    foreach (KeyValuePair<GameObject, List<GameObject>> data in control.BLOCKING_DATA.UpBlockingObjs)
                    {
                        foreach(GameObject obj in data.Value)
                        {
                            CharacterControl c = CharacterManager.Instance.
                                GetCharacter(obj.transform.root.gameObject);

                            if (c == null)
                            {
                                control.RunFunction(typeof(ClearUpVelocity));
                                break;
                            }
                            else
                            {
                                if (c.transform.position.y >
                                    control.transform.position.y + control.BOX_COLLIDER.center.y)
                                {
                                    control.RunFunction(typeof(ClearUpVelocity));
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // check down blocking objs
            if (control.RIGID_BODY.velocity.y < 0f)
            {
                control.RunFunction(typeof(CheckDownBlocking), 0.1f);
                control.RunFunction(typeof(CheckMarioStomp));
            }
            
            control.BLOCKING_DATA.FrontBlockingDicCount = control.BLOCKING_DATA.FrontBlockingObjs.Count;
            control.BLOCKING_DATA.UpBlockingDicCount = control.BLOCKING_DATA.UpBlockingObjs.Count;
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnLateUpdate()
        {
            if (control.RIGID_BODY.velocity.y >= 0f)
            {
                control.BLOCKING_DATA.MarioStompTargets.Clear();
                control.BLOCKING_DATA.DownBlockingObjs.Clear();
            }  
        }

        void CheckFrontBlocking()
        {
            control.BLOCKING_DATA.FrontBlockingObjs.Clear();

            if (!control.GetBool(typeof(ForwardReversed)))
            {
                FrontSpheresArray = control.COLLISION_SPHERE_DATA.FrontSpheres;
                FrontDirectionMultiplier = 1f;
            }
            else
            {
                FrontSpheresArray = control.COLLISION_SPHERE_DATA.BackSpheres;
                FrontDirectionMultiplier = -1f;
            }

            FrontRayDirection = this.transform.forward * FrontDirectionMultiplier;
            FrontRayLength = control.MOVE_DATA.LatestMoveForward.GetBlockDistance();

            for (int i = 0; i < FrontSpheresArray.Length; i++)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(FrontSpheresArray[i].transform.position, FrontRayDirection, FrontRayLength);

                Debug.DrawLine(FrontSpheresArray[i].transform.position, FrontSpheresArray[i].transform.position + (FrontRayDirection * FrontRayLength), Color.green);

                foreach(RaycastHit h in hits)
                {
                    if (!CollisionDetection.IgnoreCollision(control, h))
                    {
                        AddObjToDictionary.Add(control.BLOCKING_DATA.FrontBlockingObjs,
                            FrontSpheresArray[i],
                            h.collider.transform.root.gameObject);
                    }
                }
            }
        }
    }
}