using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BlockingObj : CharacterUpdate
    {
        List<CharacterControl> MarioStompTargets = new List<CharacterControl>();

        GameObject[] FrontSpheresArray;
        float FrontDirectionMultiplier;
        Vector3 FrontRayDirection = new Vector3();
        float FrontRayLength = 0f;

        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(MoveForward)))
            {
                CheckFrontBlocking();
            }

            // checking while ledge grabbing
            if (control.ANIMATION_DATA.IsRunning(typeof(MoveUp)))
            {
                if (control.ANIMATION_DATA.LatestMoveUp.Speed > 0f)
                {
                    control.RunFunction(typeof(CheckUpBlocking), 0.3f);
                }
            }
            else
            {
                // checking while jumping up
                if (control.RIGID_BODY.velocity.y > 0.001f)
                {
                    control.RunFunction(typeof(CheckUpBlocking), 0.3f);

                    foreach (KeyValuePair<GameObject, GameObject> data in control.BLOCKING_DATA.UpBlockingObjs)
                    {
                        CharacterControl c = CharacterManager.Instance.GetCharacter(
                            data.Value.transform.root.gameObject);

                        if (c == null)
                        {
                            control.RunFunction(typeof(ClearUpVelocity));
                            break;
                        }
                        else
                        {
                            if (control.transform.position.y + control.boxCollider.center.y <
                                c.transform.position.y)
                            {
                                control.RunFunction(typeof(ClearUpVelocity));
                                break;
                            }
                        }
                    }
                }
            }

            CheckMarioStomp();

            control.BLOCKING_DATA.FrontBlockingDicCount = control.BLOCKING_DATA.FrontBlockingObjs.Count;
            control.BLOCKING_DATA.UpBlockingDicCount = control.BLOCKING_DATA.UpBlockingObjs.Count;
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        void CheckMarioStomp()
        {
            if (control.RIGID_BODY.velocity.y >= 0f)
            {
                MarioStompTargets.Clear();
                control.BLOCKING_DATA.DownBlockingObjs.Clear();
                return;
            }

            if (MarioStompTargets.Count > 0)
            {
                control.RIGID_BODY.velocity = Vector3.zero;
                control.RIGID_BODY.AddForce(Vector3.up * 250f);

                foreach (CharacterControl c in MarioStompTargets)
                {
                    AttackCondition info = new AttackCondition();
                    info.CopyInfo(c.DAMAGE_DATA.MarioStompAttack, control);

                    int index = Random.Range(0, c.RAGDOLL_DATA.ArrBodyParts.Length);
                    TriggerDetector randomPart = c.RAGDOLL_DATA.ArrBodyParts[index].GetComponent<TriggerDetector>();

                    c.DAMAGE_DATA.damageTaken = new DamageTaken(
                        control,
                        c.DAMAGE_DATA.MarioStompAttack,
                        randomPart,
                        control.RIGHT_FOOT_ATTACK,
                        Vector3.zero);

                    c.DAMAGE_DATA.TakeDamage(info);
                }

                MarioStompTargets.Clear();
                return;
            }

            control.RunFunction(typeof(CheckDownBlocking), 0.1f);

            if (control.BLOCKING_DATA.DownBlockingObjs.Count > 0)
            {
                foreach (KeyValuePair<GameObject, GameObject> data in control.BLOCKING_DATA.DownBlockingObjs)
                {
                    CharacterControl c = CharacterManager.Instance.
                        GetCharacter(data.Value.transform.root.gameObject);

                    if (c != null)
                    {
                        if (c.boxCollider.center.y + c.transform.position.y < control.transform.position.y)
                        {
                            if (c != control)
                            {
                                if (!MarioStompTargets.Contains(c))
                                {
                                    MarioStompTargets.Add(c);
                                }
                            }
                        }
                    }
                }
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
            FrontRayLength = control.ANIMATION_DATA.LatestMoveForward.BlockDistance;

            for (int i = 0; i < FrontSpheresArray.Length; i++)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(FrontSpheresArray[i].transform.position, FrontRayDirection, FrontRayLength);

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