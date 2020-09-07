﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class TriggerDetector : MonoBehaviour
    {
        public CharacterControl control;
        public Collider triggerCollider;
        public Rigidbody body;

        public Vector3 LastPosition;
        public Quaternion LastRotation;

        private void Awake()
        {
            control = this.GetComponentInParent<CharacterControl>();
            triggerCollider = this.gameObject.GetComponent<Collider>();
            body = this.gameObject.GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider col)
        {
            CharacterControl attacker = CheckCollidingBodyParts(col);
        
            if (attacker != null)
            {
                TakeCollateralDamage(attacker, col);
            }
        
            CheckCollidingWeapons(col);
        }

        private void OnTriggerExit(Collider col)
        {
            CheckExitingBodyParts(col);
            CheckExitingWeapons(col);
        }

        CharacterControl CheckCollidingBodyParts(Collider col)
        {
            if (control == null)
            {
                return null;
            }

            for (int i = 0; i < control.RAGDOLL_DATA.ArrBodyParts.Length; i++)
            {
                if (control.RAGDOLL_DATA.ArrBodyParts[i].Equals(col))
                {
                    return null;
                }
            }

            CharacterControl attacker = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

            if (attacker == null)
            {
                return null;
            }

            if (col.gameObject == attacker.gameObject)
            {
                return null;
            }

            // add collider to dictionary

            if (!control.COLLIDING_OBJ_DATA.CollidingBodyParts.ContainsKey(this))
            {
                control.COLLIDING_OBJ_DATA.CollidingBodyParts.Add(this, new List<Collider>());
            }

            if (!control.COLLIDING_OBJ_DATA.CollidingBodyParts[this].Contains(col))
            {
                control.COLLIDING_OBJ_DATA.CollidingBodyParts[this].Add(col);
            }

            return attacker;
        }

        void CheckCollidingWeapons(Collider col)
        {
            MeleeWeapon w = col.transform.root.gameObject.GetComponent<MeleeWeapon>();

            if (w == null)
            {
                return;
            }

            if (w.IsThrown)
            {
                if (w.Thrower != control)
                {
                    AttackCondition info = new AttackCondition();
                    info.CopyInfo(control.DAMAGE_DATA.AxeThrow, control);

                    control.DAMAGE_DATA.damageTaken = new DamageTaken(
                        w.Thrower,
                        control.DAMAGE_DATA.AxeThrow,
                        this,
                        null,
                        Vector3.zero);

                    control.DAMAGE_DATA.TakeDamage(info);

                    if (w.FlyForward)
                    {
                        w.transform.rotation = Quaternion.Euler(0f, 90f, 45f);
                    }
                    else
                    {
                        w.transform.rotation = Quaternion.Euler(0f, -90f, 45f);
                    }

                    w.transform.parent = this.transform;

                    Vector3 offset = this.transform.position - w.AxeTip.transform.position;
                    w.transform.position += offset;

                    w.IsThrown = false;
                    return;
                }
            }
                       
            if (!control.COLLIDING_OBJ_DATA.CollidingWeapons.ContainsKey(this))
            {
                control.COLLIDING_OBJ_DATA.CollidingWeapons.Add(this, new List<Collider>());
            }

            if (!control.COLLIDING_OBJ_DATA.CollidingWeapons[this].Contains(col))
            {
                control.COLLIDING_OBJ_DATA.CollidingWeapons[this].Add(col);
            }
        }

        void CheckExitingBodyParts(Collider col)
        {
            if (control == null)
            {
                return;
            }

            if (control.COLLIDING_OBJ_DATA.CollidingBodyParts.ContainsKey(this))
            {
                if (control.COLLIDING_OBJ_DATA.CollidingBodyParts[this].Contains(col))
                {
                    control.COLLIDING_OBJ_DATA.CollidingBodyParts[this].Remove(col);
                }

                if (control.COLLIDING_OBJ_DATA.CollidingBodyParts[this].Count == 0)
                {
                    control.COLLIDING_OBJ_DATA.CollidingBodyParts.Remove(this);
                }
            }
        }

        void CheckExitingWeapons(Collider col)
        {
            if (control == null)
            {
                return;
            }

            if (control.COLLIDING_OBJ_DATA.CollidingWeapons.ContainsKey(this))
            {
                if (control.COLLIDING_OBJ_DATA.CollidingWeapons[this].Contains(col))
                {
                    control.COLLIDING_OBJ_DATA.CollidingWeapons[this].Remove(col);
                }

                if (control.COLLIDING_OBJ_DATA.CollidingWeapons[this].Count == 0)
                {
                    control.COLLIDING_OBJ_DATA.CollidingWeapons.Remove(this);
                }
            }
        }

        void TakeCollateralDamage(CharacterControl attacker, Collider col)
        {
            if (attacker.RAGDOLL_DATA.flyingRagdollData.IsTriggered)
            {
                if (attacker.RAGDOLL_DATA.flyingRagdollData.Attacker != control)
                {
                    float mag = Vector3.SqrMagnitude(col.attachedRigidbody.velocity);
                    Debug.Log("incoming ragdoll: " + attacker.gameObject.name + "\n" + "Velocity: " + mag);

                    if (mag >= 10f)
                    {
                        control.DAMAGE_DATA.damageTaken = new DamageTaken(
                            null,
                            null,
                            this,
                            null,
                            col.attachedRigidbody.velocity);

                        control.DAMAGE_DATA.hp = 0;
                        control.RAGDOLL_DATA.RagdollTriggered = true;
                    }
                }
            }
        }
    }
}