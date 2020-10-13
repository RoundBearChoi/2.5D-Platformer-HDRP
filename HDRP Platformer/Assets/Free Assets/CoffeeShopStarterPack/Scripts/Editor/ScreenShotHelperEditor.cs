// ******------------------------------------------------------******
// ScreenShotHelperEditor.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ScreenShotHelper))]
public class ScreenShotHelperEditor : Editor
{
    SerializedProperty lastScreenShot; 
    SerializedProperty isMultipleObject;
    SerializedProperty currentGameObject;
    SerializedProperty UITestImage;
    SerializedProperty ScreenShotPath;

    /// <summary>
    /// bool used for forcing a folder import refresh
    /// </summary>
    bool isAssetDatabaseDirty = false;

    private void OnEnable()
    {
        lastScreenShot   = serializedObject.FindProperty("lastScreenshot");
        isMultipleObject = serializedObject.FindProperty("isMultipleObject");
        currentGameObject = serializedObject.FindProperty("currentGameObject");
        UITestImage = serializedObject.FindProperty("testImage");
        ScreenShotPath = serializedObject.FindProperty("ScreenShotPath");
        isAssetDatabaseDirty = false;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(lastScreenShot);
        EditorGUILayout.PropertyField(isMultipleObject);
        EditorGUILayout.PropertyField(currentGameObject);
        EditorGUILayout.PropertyField(UITestImage);

        ScreenShotPath.stringValue = EditorGUILayout.TextField("Screenshot Path:", ScreenShotPath.stringValue);

        if (GUILayout.Button("TakeScreenShot"))
        {
            var helper = serializedObject.targetObject as ScreenShotHelper;
            helper.TakeScreenShot();
            isAssetDatabaseDirty = true;
        }

        serializedObject.ApplyModifiedProperties();
        //When we create new screenshots we need to force editor to import them.
        if (isAssetDatabaseDirty)
            AssetDatabase.Refresh();
    }
}
