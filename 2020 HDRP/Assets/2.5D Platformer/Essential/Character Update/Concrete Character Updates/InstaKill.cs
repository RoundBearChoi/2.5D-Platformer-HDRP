using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class InstaKill : CharacterUpdate
    {
        [SerializeField] RuntimeAnimatorController Assassination_A;
        [SerializeField] RuntimeAnimatorController Assassination_B;

        public override void InitComponent()
        {
            control.INSTA_KILL_DATA.Animation_A = Assassination_A;
            control.INSTA_KILL_DATA.Animation_B = Assassination_B;
            control.INSTA_KILL_DATA.DeathByInstaKill = DeathByInstaKill;
        }

        public override void OnFixedUpdate()
        {
            if (control.GetUpdater(typeof(ManualInput)))
            {
                return;
            }

            if (!control.characterSetup.SkinnedMeshAnimator.
                GetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
            {
                return;
            }

            foreach (KeyValuePair<TriggerDetector, List<Collider>> data in control.COLLIDING_OBJ_DATA.CollidingBodyParts)
            {
                foreach (Collider col in data.Value)
                {
                    CharacterControl c = CharacterManager.Instance.GetCharacter(col.transform.root.gameObject);

                    if (c == control)
                    {
                        continue;
                    }

                    if (c.GetUpdater(typeof(ManualInput)) == null)
                    {
                        continue;
                    }

                    if (!c.characterSetup.SkinnedMeshAnimator.
                        GetBool(HashManager.Instance.ArrMainParams[(int)MainParameterType.Grounded]))
                    {
                        continue;
                    }

                    if (c.ANIMATION_DATA.IsRunning(typeof(Attack)))
                    {
                        continue;
                    }

                    if (control.ANIMATION_DATA.IsRunning(typeof(Attack)))
                    {
                        continue;
                    }

                    if (c.GetBool(typeof(StateNameContains), "RunningSlide"))//.StateNameContains("RunningSlide"))
                    {
                        continue;
                    }

                    if (c.DAMAGE_DATA.IsDead())
                    {
                        continue;
                    }

                    if (control.DAMAGE_DATA.IsDead())
                    {
                        continue;
                    }

                    //Debug.Log("instaKill");
                    //c.INSTA_KILL_DATA.DeathByInstaKill(control);

                    return;
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

        void DeathByInstaKill(CharacterControl attacker)
        {
            control.ANIMATION_DATA.CurrentRunningAbilities.Clear();
            attacker.ANIMATION_DATA.CurrentRunningAbilities.Clear();

            control.RIGID_BODY.useGravity = false;
            control.boxCollider.enabled = false;
            control.characterSetup.
                SkinnedMeshAnimator.runtimeAnimatorController = control.INSTA_KILL_DATA.Animation_B;

            attacker.RIGID_BODY.useGravity = false;
            attacker.boxCollider.enabled = false;
            attacker.characterSetup.
                SkinnedMeshAnimator.runtimeAnimatorController = control.INSTA_KILL_DATA.Animation_A;

            Vector3 dir = control.transform.position - attacker.transform.position;

            if (dir.z < 0f)
            {
                attacker.RunFunction(typeof(FaceForward), false);// ROTATION_DATA.FaceForward(false);
            }
            else if (dir.z > 0f)
            {
                attacker.RunFunction(typeof(FaceForward), true);// ROTATION_DATA.FaceForward(true);
            }

            control.transform.LookAt(control.transform.position + (attacker.transform.forward * 5f), Vector3.up);
            control.transform.position = attacker.transform.position + (attacker.transform.forward * 0.45f);

            control.DAMAGE_DATA.hp = 0f;
        }
    }
}