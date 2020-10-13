// ******------------------------------------------------------******
// SliceHelperEditor.cs
// Editor script to help arrange sliced items i.e. cheesecakes
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

namespace PW
{
    [CustomEditor(typeof(SliceHelper))]
    [CanEditMultipleObjects]
    public class SliceHelperEditor : Editor
	{
        SerializedProperty slicePrefab;

        SerializedProperty sliceCount;

        void OnEnable()
        {
            slicePrefab = serializedObject.FindProperty("slicePrefab");
            sliceCount = serializedObject.FindProperty("sliceCount");

        }

        public override void OnInspectorGUI()
		{
            serializedObject.Update();

            EditorGUILayout.PropertyField(slicePrefab);
            EditorGUILayout.PropertyField(sliceCount);

            if (GUILayout.Button("Arrange Slices"))
			{
				Debug.Log("Arranging slices");
                if (slicePrefab != null && slicePrefab.objectReferenceValue != null)
                {
                    var selected = Selection.objects[0] as GameObject;
                    List<GameObject> existedChildrenObjects= new List<GameObject>();
                    for (int c = 0; c < selected.transform.childCount; c++)
                    {
                        existedChildrenObjects.Add(selected.transform.GetChild(c).gameObject);
                    }

                    var selectedPrefabGo = slicePrefab.objectReferenceValue as GameObject;
                    for (int i = 0; i < sliceCount.intValue; i++)
                    {
                        var rotNow = i * 36;
                        var go = new GameObject("Slice" + i);
                        go.transform.position = Vector3.zero;
                        go.transform.rotation = Quaternion.identity;

                        go.transform.SetParent(selected.transform);
                        go.transform.localPosition= Vector3.zero;
                        go.transform.rotation = Quaternion.identity;
                        var slice = GameObject.Instantiate(slicePrefab.objectReferenceValue, go.transform) as GameObject;
                        slice.gameObject.SetActive(true);
                        go.transform.rotation = Quaternion.Euler(0, rotNow, 0f);
                        slice.transform.SetParent(selected.transform);
                        //Destroy the helper parent
                        DestroyImmediate(go);
                    }
                    selectedPrefabGo.SetActive(false);
                    //Destroy the prefab base objects from the element
                    foreach (var go in existedChildrenObjects)
                    {
                        DestroyImmediate(go);
                    }
                }
                
			}

            serializedObject.ApplyModifiedProperties();
		}
	}
}
