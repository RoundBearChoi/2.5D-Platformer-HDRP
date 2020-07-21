using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ConditionCheck_Grounded : CheckConditionBase
    {
        public override bool MeetsCondition(CharacterControl control)
        {
            if (control.characterSetup.SkinnedMeshAnimator.
                                GetBool(HashManager.Instance.ArrMainParams[
                                    (int)MainParameterType.Grounded]) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
