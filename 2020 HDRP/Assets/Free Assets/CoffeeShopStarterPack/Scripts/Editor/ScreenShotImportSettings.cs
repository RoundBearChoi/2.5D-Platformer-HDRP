// ******------------------------------------------------------******
// ScreenShotImportSettings.cs
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

public class ScreenShotImportSettings : AssetPostprocessor
{
    /// <summary>
    /// We automagically look for textures in ScreenShots folder
    /// to set them as sprites so that you can use it in productManager and ordergenerator.
    /// </summary>
    ///
    const string folderNamesToLook = "/ScreenShots/";

    void OnPreprocessTexture()
    {
        // Only post process textures if they are in a folder
        // "ScreenShots" or a sub folder of it.
        if (assetPath.IndexOf(folderNamesToLook) == -1)
            return;

        Debug.Log("We use this AssetPostProcessign for screenshots folder and override some import settings for this folder, please delete this script if you don't want this automation tool");

        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.textureType = TextureImporterType.Sprite;

        textureImporter.spritePixelsPerUnit = 100;
        textureImporter.spriteImportMode = SpriteImportMode.Single;

        textureImporter.spritePivot = new Vector2(0.5f, 0.5f);

        textureImporter.sRGBTexture = true;
    }
}
