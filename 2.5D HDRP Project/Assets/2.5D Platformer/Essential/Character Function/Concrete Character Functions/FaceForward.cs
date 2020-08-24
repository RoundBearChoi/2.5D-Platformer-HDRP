using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class FaceForward : CharacterFunction
    {
        static string TutorialScene_CharacterSelect = "TutorialScene_CharacterSelect";

        public override void RunFunction(bool forward)
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
    }
}