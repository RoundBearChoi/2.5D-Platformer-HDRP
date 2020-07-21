using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BlockingObj : CharacterUpdate
    {
        Dictionary<GameObject, GameObject> UpBlockingObjs = new Dictionary<GameObject, GameObject>();
        

        List<CharacterControl> MarioStompTargets = new List<CharacterControl>();
        List<GameObject> FrontBlockingObjsList = new List<GameObject>();

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
                    CheckUpBlocking();
                }
            }
            else
            {
                // checking while jumping up
                if (control.RIGID_BODY.velocity.y > 0.001f)
                {
                    CheckUpBlocking();

                    foreach (KeyValuePair<GameObject, GameObject> data in UpBlockingObjs)
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
                else
                {
                    if (UpBlockingObjs.Count != 0)
                    {
                        UpBlockingObjs.Clear();
                    }
                }
            }

            CheckMarioStomp();

            control.BLOCKING_DATA.FrontBlockingDicCount = control.BLOCKING_DATA.FrontBlockingObjs.Count;
            control.BLOCKING_DATA.UpBlockingDicCount = UpBlockingObjs.Count;
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

            CheckDownBlocking();

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
                        AddBlockingObjToDic(control.BLOCKING_DATA.FrontBlockingObjs,
                            FrontSpheresArray[i],
                            h.collider.transform.root.gameObject);
                    }
                }
            }
        }

        void CheckDownBlocking()
        {
            control.BLOCKING_DATA.DownBlockingObjs.Clear();

            foreach (GameObject obj in control.COLLISION_SPHERE_DATA.BottomSpheres)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(obj.transform.position, Vector3.down, 0.1f);

                foreach (RaycastHit h in hits)
                {
                    if (!CollisionDetection.IgnoreCollision(control, h))
                    {
                        AddBlockingObjToDic(control.BLOCKING_DATA.DownBlockingObjs,
                            obj,
                            h.collider.transform.root.gameObject);
                    }
                }
            }
        }

        void CheckUpBlocking()
        {
            foreach (GameObject o in control.COLLISION_SPHERE_DATA.UpSpheres)
            {
                GameObject blockingObj = CollisionDetection.GetCollidingObject(control, o, this.transform.up, 0.3f,
                    ref control.BLOCKING_DATA.RaycastContact);

                if (blockingObj != null)
                {
                    AddBlockingObjToDic(UpBlockingObjs, o, blockingObj);
                }
                else
                {
                    RemoveBlockingObjFromDic(UpBlockingObjs, o);
                }
            }
        }

        void AddBlockingObjToDic(Dictionary<GameObject, GameObject> dic, GameObject key, GameObject value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }

        void RemoveBlockingObjFromDic(Dictionary<GameObject, GameObject> dic, GameObject key)
        {
            if (dic.ContainsKey(key))
            {
                dic.Remove(key);
            }
        }

        List<GameObject> GetFrontBlockingObjList()
        {
            FrontBlockingObjsList.Clear();

            foreach(KeyValuePair<GameObject, GameObject> data in control.BLOCKING_DATA.FrontBlockingObjs)
            {
                if (!FrontBlockingObjsList.Contains(data.Value))
                {
                    FrontBlockingObjsList.Add(data.Value);
                }
            }

            return FrontBlockingObjsList;
        }
    }
}