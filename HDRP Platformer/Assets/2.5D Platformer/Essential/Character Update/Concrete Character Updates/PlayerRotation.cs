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
                if (control.ROTATION_DATA.LockTurn)
                {
                    AnimatorStateInfo info = control.characterSetup.
                        SkinnedMeshAnimator.GetCurrentAnimatorStateInfo(0);

                    if (info.normalizedTime >= control.ROTATION_DATA.UnlockTiming)
                    {
                        control.ROTATION_DATA.LockTurn = false;
                    }
                }
            }
        }
    }
}