using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class HashTool
    {
        public static void Add(System.Enum e, int[] intArray)
        {
            int count = System.Enum.GetValues(e.GetType()).Length;

            for (int i = 0; i < count; i++)
            {
                intArray[i] = Animator.StringToHash(((MainParameterType)i).ToString());
            }
        }

        public static int GetMaxValue(System.Type enumType)
        {
            return System.Enum.GetValues(enumType).Length;
        }
    }
}