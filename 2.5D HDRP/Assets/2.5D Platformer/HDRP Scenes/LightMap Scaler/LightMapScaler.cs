using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LightMapScaler : MonoBehaviour
    {
        public float Scale;

        public void ScaleAll()
        {
            Debug.Log("---Changing ALL LightMap Scale---");

            MeshRenderer[] arr = FindObjectsOfType<MeshRenderer>();

            foreach(MeshRenderer r in arr)
            {
                Debug.Log(r.gameObject.name);
                r.scaleInLightmap = Scale;
            }
        }
    }
}