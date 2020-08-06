using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class AddObjToDictionary
    {
        public static void Add(Dictionary<GameObject, List<GameObject>> dic, GameObject key, GameObject value)
        {
            if (dic.ContainsKey(key))
            {
                if (!dic[key].Contains(value))
                {
                    dic[key].Add(value);
                }
            }
            else
            {
                dic.Add(key, new List<GameObject>());
                dic[key].Add(value);
            }
        }
    }
}