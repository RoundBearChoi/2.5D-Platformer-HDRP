using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class Ragdoll : CharacterUpdate
    {
        RagdollData RDATA => control.DATASET.RAGDOLL_DATA;

        public override void InitComponent()
        {
            SetupBodyParts();
        }

        public override void OnFixedUpdate()
        {
            if (RDATA.RagdollTriggered)
            {
                ProcRagdoll();
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

        public void SetupBodyParts()
        {
            List<Collider> BodyParts = new List<Collider>();
            Collider[] colliders = control.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != control.gameObject)
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null &&
                        c.gameObject.GetComponent<LedgeCollider>() == null)
                    {
                        c.isTrigger = true;
                        BodyParts.Add(c);
                        c.attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                        c.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;

                        CharacterJoint joint = c.GetComponent<CharacterJoint>();
                        if (joint != null)
                        {
                            joint.enableProjection = true;
                        }

                        if (c.GetComponent<TriggerDetector>() == null)
                        {
                            c.gameObject.AddComponent<TriggerDetector>();
                        }
                    }
                }
            }

            RDATA.ArrBodyParts = new Collider[BodyParts.Count];

            for (int i = 0; i < RDATA.ArrBodyParts.Length; i++)
            {
                RDATA.ArrBodyParts[i] = BodyParts[i];
            }
        }

        void ProcRagdoll()
        {
            RDATA.RagdollTriggered = false;

            if (control.ANIMATOR.avatar == null)
            {
                return;
            }

            //change layers
            Transform[] arr = control.gameObject.GetComponentsInChildren<Transform>();
            foreach (Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer(RB_Layers.DEADBODY.ToString());
            }

            //save bodypart positions
            for (int i = 0; i < RDATA.ArrBodyParts.Length; i++)
            {
                TriggerDetector det = RDATA.ArrBodyParts[i].GetComponent<TriggerDetector>();
                det.LastPosition = RDATA.ArrBodyParts[i].gameObject.transform.position;
                det.LastRotation = RDATA.ArrBodyParts[i].gameObject.transform.rotation;
            }

            //turn off animator/avatar
            control.RIGID_BODY.useGravity = false;
            control.RIGID_BODY.velocity = Vector3.zero;
            control.gameObject.GetComponent<BoxCollider>().enabled = false;
            control.ANIMATOR.enabled = false;
            control.ANIMATOR.avatar = null;

            //turn off ledge colliders
            control.RunFunction(typeof(LedgeCollidersOff));

            //turn on ragdoll
            for (int i = 0; i < RDATA.ArrBodyParts.Length; i++)
            {
                RDATA.ArrBodyParts[i].isTrigger = false;
                RDATA.ArrBodyParts[i].attachedRigidbody.isKinematic = true;
            }

            for (int i = 0; i < RDATA.ArrBodyParts.Length; i++)
            {
                TriggerDetector det = RDATA.ArrBodyParts[i].GetComponent<TriggerDetector>();
                RDATA.ArrBodyParts[i].attachedRigidbody.MovePosition(det.LastPosition);
                RDATA.ArrBodyParts[i].attachedRigidbody.MoveRotation(det.LastRotation);
                RDATA.ArrBodyParts[i].attachedRigidbody.velocity = Vector3.zero;
            }

            for (int i = 0; i < RDATA.ArrBodyParts.Length; i++)
            {
                RDATA.ArrBodyParts[i].attachedRigidbody.isKinematic = false;
            }

            control.RunFunction(typeof(ClearRagdollVelocity));
            
            if (control.DATASET.DAMAGE_DATA.damageTaken != null)
            {
                //take damage from ragdoll
                Vector3 incomingVelocity = control.DATASET.DAMAGE_DATA.damageTaken.INCOMING_VELOCITY;
                TriggerDetector damagedPart = control.DATASET.DAMAGE_DATA.damageTaken.DAMAGEE;

                if (Vector3.SqrMagnitude(incomingVelocity) > 0.0001f)
                {
                    Debug.Log(control.gameObject.name + ": taking damage from ragdoll");
                    damagedPart.body.AddForce(incomingVelocity * 0.7f);
                }

                //take damage from attack
                else
                {
                    control.RunFunction(typeof(AddForceToDamagedPart), RagdollPushType.NORMAL);
                }
            }
        }
    }
}