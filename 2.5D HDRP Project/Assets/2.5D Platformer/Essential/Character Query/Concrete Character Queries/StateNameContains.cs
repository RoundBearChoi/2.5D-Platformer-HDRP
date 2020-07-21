using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class StateNameContains : CharacterQuery
    {
        public override bool ReturnBool(string str)
        {
            AnimatorClipInfo[] arr = control.characterSetup.SkinnedMeshAnimator.GetCurrentAnimatorClipInfo(0);

            foreach (AnimatorClipInfo clipInfo in arr)
            {
                if (clipInfo.clip.name.Contains(str))
                {
                    return true;
                }
            }

            return false;
        }
    }
}