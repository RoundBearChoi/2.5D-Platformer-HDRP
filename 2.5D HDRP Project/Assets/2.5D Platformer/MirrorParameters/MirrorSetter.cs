using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class MirrorSetter
    {
        public static void SetMirrorParameter(CharacterControl control, TransitionTarget transitionTo)
        {
            MirrorParameterType mirrorParamType = transitionTo.GetNextMirrorType();

            if (mirrorParamType == MirrorParameterType.idle_mirror)
            {
                if (control.GetBool(typeof(RightFootIsForward)))
                {
                    ToggleMirror(control, mirrorParamType, true);
                }
                else
                {
                    ToggleMirror(control, mirrorParamType, false);
                }
            }

            if (mirrorParamType == MirrorParameterType.idlepivot_mirror)
            {
                if (control.GetBool(typeof(RightFootIsForward)))
                {
                    ToggleMirror(control, mirrorParamType, true);
                }
                else
                {
                    ToggleMirror(control, mirrorParamType, false);
                }
            }

            if (mirrorParamType == MirrorParameterType.runstart_mirror)
            {
                if (control.GetBool(typeof(RightFootIsForward)))
                {
                    ToggleMirror(control, mirrorParamType, true);
                }
                else
                {
                    ToggleMirror(control, mirrorParamType, false);
                }
            }

            if (mirrorParamType == MirrorParameterType.runstop_mirror)
            {
                if (control.GetBool(typeof(RightFootIsForward)))
                {
                    ToggleMirror(control, mirrorParamType, false);
                }
                else
                {
                    ToggleMirror(control, mirrorParamType, true);
                }
            }
        }

        static void ToggleMirror(CharacterControl control, MirrorParameterType mirrorParamType, bool toogle)
        {
            control.characterSetup.SkinnedMeshAnimator.SetBool(
                HashManager.Instance.ArrMirrorParameters[(int)mirrorParamType], toogle);
        }
    }
}