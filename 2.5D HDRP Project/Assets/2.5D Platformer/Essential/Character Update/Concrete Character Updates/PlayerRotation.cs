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
            control.ROTATION_DATA.FaceForward = FaceForward;
            control.ROTATION_DATA.IsFacingForward = IsFacingForward;
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

        void FaceForward(bool forward)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(TutorialScene_CharacterSelect))
            {
                return;
            }

            if (!control.characterSetup.SkinnedMeshAnimator.enabled)
            {
                return;
            }

            if (control.ROTATION_DATA.LockTurn)
            {
                return;
            }

            if (forward)
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        bool IsFacingForward()
        {
            if (control.transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void ClearTurnLock()
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(LockTurn)))
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