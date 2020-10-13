using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Roundbeargames
{
    [CustomEditor(typeof(LightMapScaler))]
    public class LightMapScalerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LightMapScaler scaler = (LightMapScaler)target;

            if (GUILayout.Button("Scale All Renderers"))
            {
                scaler.ScaleAll();
            }
        }
    }
}

