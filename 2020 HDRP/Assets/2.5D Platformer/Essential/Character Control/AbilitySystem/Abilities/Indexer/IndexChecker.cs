﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class IndexChecker
    {
        public static bool MakeTransition(CharacterControl control, List<TransitionConditionType> transitionConditions)
        {
            foreach (TransitionConditionType c in transitionConditions)
            {
                CheckConditionBase check = GetConditionChecker.GET(c);

                if (!check.MeetsCondition(control))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool NotCondition(CharacterControl control, List<TransitionConditionType> notConditions)
        {
            foreach (TransitionConditionType c in notConditions)
            {
                CheckConditionBase check = GetConditionChecker.GET(c);

                if (check.MeetsCondition(control))
                {
                    return true;
                }
            }

            return false;
        }
    }
}