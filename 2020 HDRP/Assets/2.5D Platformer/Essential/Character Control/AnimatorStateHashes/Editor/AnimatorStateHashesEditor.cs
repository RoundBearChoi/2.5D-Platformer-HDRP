using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Roundbeargames
{
    [CustomEditor(typeof(AnimatorStateHashes))]
    public class AnimatorStateHashesEditor : Editor
    {
        SerializedProperty StateTypes;
        SerializedProperty StateNames;
        bool ShowDefault = false;

        private void OnEnable()
        {
            StateTypes = serializedObject.FindProperty("HashTypes");
            StateNames = serializedObject.FindProperty("StateNames");
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Show Default Layout"))
            {
                ShowDefault = true;
            }
            if (GUILayout.Button("Show Horizontal Layout"))
            {
                ShowDefault = false;
            }

            EditorGUILayout.Space(20);

            if (ShowDefault)
            {
                DrawDefaultInspector();
            }
            else
            {
                DrawHorizontalAnimatorStateHashes();
            }
        }

        void DrawHorizontalAnimatorStateHashes()
        {
            serializedObject.Update();
            
            GUILayout.BeginHorizontal("animator_state_hashes");

            EditorGUILayout.PropertyField(StateTypes, new GUIContent("State Types"));

            EditorGUILayout.PropertyField(StateNames, new GUIContent("State Names"));

            GUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }
    }
}