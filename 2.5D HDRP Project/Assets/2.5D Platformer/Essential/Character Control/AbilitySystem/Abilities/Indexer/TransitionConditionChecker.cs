using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class TransitionConditionChecker
    {
        public static bool MakeTransition(CharacterControl control, List<TransitionConditionType> transitionConditions)
        {
            foreach (TransitionConditionType c in transitionConditions)
            {
                CheckCondition check = GetConditionChecker.GET(c);

                if (!check.MeetsCondition(control))
                {
                    return false;
                }
            }

            return true;
        }
    }
}