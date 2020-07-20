﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class BlockingObj : CharacterUpdate
    {
        Dictionary<GameObject, GameObject> UpBlockingObjs = new Dictionary<GameObject, GameObject>();
        Dictionary<GameObject, GameObject> DownBlockingObjs = new Dictionary<GameObject, GameObject>();

        List<CharacterControl> MarioStompTargets = new List<CharacterControl>();
        List<GameObject> FrontBlockingObjsList = new List<GameObject>();

        GameObject[] FrontSpheresArray;
        float DirBlock;

        public override void InitComponent()
        {
            characterUpdateProcessor.ArrCharacterUpdate[(int)CharacterUpdateType.BLOCKINGOBJECTS] = this;
        }

        public override void OnFixedUpdate()
        {
            if (control.ANIMATION_DATA.IsRunning(typeof(MoveForward)))
            {
                CheckFrontBlocking();
            }
            else
            {
                if (control.BLOCKING_DATA.FrontBlockingObjs.Count != 0)
                {
                    control.BLOCKING_DATA.FrontBlockingObjs.Clear();
                }
            }

            // checking while ledge grabbing
            if (control.ANIMATION_DATA.IsRunning(typeof(MoveUp)))
            {
                if (control.animationProgress.LatestMoveUp.Speed > 0f)
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
                            control.animationProgress.NullifyUpVelocity();
                            break;
                        }
                        else
                        {
                            if (control.transform.position.y + control.boxCollider.center.y <
                                c.transform.position.y)
                            {
                                control.animationProgress.NullifyUpVelocity();
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
                DownBlockingObjs.Clear();
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
                        control.RightFoot_Attack,
                        Vector3.zero);

                    c.DAMAGE_DATA.TakeDamage(info);
                }

                MarioStompTargets.Clear();
                return;
            }

            CheckDownBlocking();

            if (DownBlockingObjs.Count > 0)
            {
                foreach (KeyValuePair<GameObject, GameObject> data in DownBlockingObjs)
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
            if (!control.animationProgress.ForwardIsReversed())
            {
                FrontSpheresArray = control.COLLISION_SPHERE_DATA.FrontSpheres;
                DirBlock = 1f;

                foreach (GameObject s in control.COLLISION_SPHERE_DATA.BackSpheres)
                {
                    if (control.BLOCKING_DATA.FrontBlockingObjs.ContainsKey(s))
                    {
                        control.BLOCKING_DATA.FrontBlockingObjs.Remove(s);
                    }
                }
            }
            else
            {
                FrontSpheresArray = control.COLLISION_SPHERE_DATA.BackSpheres;
                DirBlock = -1f;

                foreach (GameObject s in control.COLLISION_SPHERE_DATA.FrontSpheres)
                {
                    if (control.BLOCKING_DATA.FrontBlockingObjs.ContainsKey(s))
                    {
                        control.BLOCKING_DATA.FrontBlockingObjs.Remove(s);
                    }
                }
            }

            for (int i = 0; i < FrontSpheresArray.Length; i++)
            {
                GameObject blockingObj = CollisionDetection.GetCollidingObject(
                    control, FrontSpheresArray[i], this.transform.forward * DirBlock,
                    control.animationProgress.LatestMoveForward.BlockDistance,
                    ref control.BLOCKING_DATA.RaycastContact);

                if (blockingObj != null)
                {
                    AddBlockingObjToDic(control.BLOCKING_DATA.FrontBlockingObjs, FrontSpheresArray[i], blockingObj);
                }
                else
                {
                    RemoveBlockingObjFromDic(control.BLOCKING_DATA.FrontBlockingObjs, FrontSpheresArray[i]);
                }
            }
        }

        void CheckDownBlocking()
        {
            foreach (GameObject o in control.COLLISION_SPHERE_DATA.BottomSpheres)
            {
                GameObject blockingObj = CollisionDetection.GetCollidingObject(control, o, Vector3.down, 0.1f,
                    ref control.BLOCKING_DATA.RaycastContact);

                if (blockingObj != null)
                {
                    AddBlockingObjToDic(DownBlockingObjs, o, blockingObj);
                }
                else
                {
                    RemoveBlockingObjFromDic(DownBlockingObjs, o);
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