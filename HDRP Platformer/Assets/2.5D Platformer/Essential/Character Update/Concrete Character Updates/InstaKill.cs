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

                    if (c.UpdatingAbility(typeof(Attack)))
                    {
                        continue;
                    }

                    if (control.UpdatingAbility(typeof(Attack)))
                    {
                        continue;
                    }

                    //if (c.GetBool(typeof(StateNameContains), "RunningSlide"))
                    //{
                    //    continue;
                    //}

                    if (c.GetBool(typeof(CharacterDead)))
                    {
                        continue;
                    }

                    if (control.GetBool(typeof(CharacterDead)))
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
    }
}