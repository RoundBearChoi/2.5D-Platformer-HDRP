using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

namespace Roundbeargames
{
    [CustomEditor(typeof(AnimatorStateHashes))]
    public class AnimatorStateHashesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space(15);

            GUI.backgroundColor = Color.green;

            if (GUILayout.Button("Get Short Name Hashes"))
            {

            }
        }
    }
}

