using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GetChildObj : CharacterQuery
    {
        Dictionary<string, GameObject> ChildObjects = new Dictionary<string, GameObject>();

        public override GameObject ReturnGameObj(string name)
        {
            if (ChildObjects.ContainsKey(name))
            {
                return ChildObjects[name];
            }

            Transform[] arr = control.gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                if (t.gameObject.name.Equals(name))
                {
                    ChildObjects.Add(name, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }
    }
}