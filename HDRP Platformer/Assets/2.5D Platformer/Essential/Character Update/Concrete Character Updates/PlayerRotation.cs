using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerRotation : CharacterUpdate
    {
        static string TutorialScene_CharacterSelect = "TutorialScene_CharacterSelect";

        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            ClearTurnLock();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }

        void ClearTurnLock()
        {
            if (!control.UpdatingAbility(typeof(LockTurn)))
            {
                if (control.DATASET.ROTATION_DATA.LockTurn)
                {
                    AnimatorStateInfo info = control.ANIMATOR.GetCurrentAnimatorStateInfo(0);

                    if (info.normalizedTime >= control.DATASET.ROTATION_DATA.UnlockTiming)
                    {
                        control.DATASET.ROTATION_DATA.LockTurn = false;
                    }
                }
            }
        }
    }
}