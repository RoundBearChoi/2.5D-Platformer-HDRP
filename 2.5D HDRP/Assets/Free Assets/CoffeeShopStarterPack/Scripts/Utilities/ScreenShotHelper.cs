// ******------------------------------------------------------******
// ScreenShotHelper.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShotHelper : MonoBehaviour {
    [SerializeField]
    Texture2D lastScreenshot;
    [SerializeField]
    public GameObject currentGameObject;
    [SerializeField]
    public UnityEngine.UI.RawImage testImage;
    [SerializeField]
    public bool isMultipleObject;
    [SerializeField]
    public string ScreenShotPath;

    public void TakeScreenShot()
    {
        //Create and set the screenshotCamera

        GameObject newObj = new GameObject();
        Camera screenShotCam = newObj.AddComponent<Camera>();

#if UNITY_POST_PROCESSING_STACK_V2

        //If you want to  use the post processing stack as we did,
        //You can set it here, change settings.
        
        var postProcessLayerComponent = newObj.AddComponent<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();

        //Choose the layer to your post process volume layer.
        //We named ours PostProcessLayer.

        postProcessLayerComponent.volumeLayer = LayerMask.GetMask(new string[1] { "PostProcessLayer" });
        postProcessLayerComponent.volumeTrigger = screenShotCam.transform;

#endif
        screenShotCam.fieldOfView = 60;
        screenShotCam.nearClipPlane = 0.03f;
        screenShotCam.nearClipPlane = .03f;
        screenShotCam.farClipPlane = 400f;
        screenShotCam.clearFlags = CameraClearFlags.Color;
        screenShotCam.backgroundColor = Color.cyan;
        screenShotCam.aspect = 1.0f;
        screenShotCam.name = "ScreenShotCam";

        //Deativate all and activate the currentObject before taking screenshots.
        for (int t = 0; t < transform.childCount; t++)
        {
            transform.GetChild(t).gameObject.SetActive(false);
        }

        if (isMultipleObject)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                if (i > 0)
                    transform.GetChild(i - 1).gameObject.SetActive(false);
                currentGameObject = transform.GetChild(i).gameObject;
                currentGameObject.SetActive(true);
                TakeSingleScreenShot(screenShotCam);
            }
        }
        else
        {
            currentGameObject.SetActive(true);
            TakeSingleScreenShot(screenShotCam);
        }
        //Destroy the object that holds our screenshotCamera
        DestroyImmediate(newObj);
    }

    public void TakeSingleScreenShot(Camera screenShotCam)
    {
        // capture the screenshot and and save it as a square PNG.
        int sqr = 512;
        Texture2D virtualPhoto = new Texture2D(sqr, sqr, TextureFormat.RGB24, false);

        virtualPhoto = PositionCamAndGetScreenTexture(screenShotCam,sqr);

        lastScreenshot = new Texture2D(sqr, sqr, TextureFormat.RGB24, false);
        Graphics.CopyTexture(virtualPhoto, lastScreenshot);
        lastScreenshot.Apply();
        GL.Clear(true, true, Color.clear);

        //If we want to show a preview, we do it here
        if(testImage!=null)
        testImage.texture = lastScreenshot;

        // Save to path if needed
        byte[] bytes;
        bytes = virtualPhoto.EncodeToPNG();
        //in case user didn't set a directory for screenshots use the default path.
        //We dont use the UI folder because we don't want to override the initial screenshots, change it if you want to force this.

        var dir = "Assets/CoffeeShopStarterPack/Test/ScreenShots/";
        if (!string.IsNullOrEmpty(ScreenShotPath))
            dir = ScreenShotPath;

        //IO operations can result with unexpected exceptions. we check it here.
        try
        {
            //Create directory if it doesn't exist;
            Directory.CreateDirectory(dir);
            string fPath = dir + currentGameObject.name + ".png";
            System.IO.File.WriteAllBytes(fPath, bytes);
        }
        catch(System.Exception e)
        {
            Debug.LogError("Something wrong happened"+ e.Message);
        }
        
    }

    public Texture2D PositionCamAndGetScreenTexture(Camera screenShotCam,int sqr = 50)
    {
        //Get object extents

        Vector3 objBounds = currentGameObject.transform.EncapsulateBounds().extents;
        var boundsMagnitude = objBounds.magnitude;
        var radius = 1f;
        var distance = (boundsMagnitude / (Mathf.Tan(0.5f *screenShotCam.fieldOfView * Mathf.Deg2Rad)) * radius);
        
        var maxZoomIn = distance * 5f;
        var maxZoomOut = distance * 10f;

        Vector3 posVector = new Vector3(0f, -.1f,  -.1f)* (maxZoomIn + maxZoomOut);

        Quaternion cameraRot = Quaternion.Euler(45f, -180f, 0f);

        //position camera to a place we can see the object
        screenShotCam.transform.position = currentGameObject.transform.position - posVector;

        //rotate camera to look at the front of object
        screenShotCam.transform.rotation = cameraRot;

        //We create temporary renderTexture to save our screenshot from camera

        RenderTexture tempRT = new RenderTexture(sqr, sqr, 24);
        screenShotCam.targetTexture = tempRT;
        screenShotCam.Render();
        RenderTexture.active = tempRT;

        Texture2D virtualPhoto = new Texture2D(sqr, sqr, TextureFormat.RGB24, false);
        virtualPhoto.ReadPixels(new Rect(0, 0, sqr, sqr), 0, 0);
        virtualPhoto.Apply();

        //Cleanup
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;
        screenShotCam.targetTexture = null;

        //We destroy the temporary render.
        DestroyImmediate(tempRT);

        return virtualPhoto;
    }
}

public static class TransformExtensions
{
    public static Bounds EncapsulateBounds(this Transform transform)
    {
        var bounds = new Bounds();
        var renderers = transform.GetComponentsInChildren<Renderer>();
        if (renderers != null)
        {
            foreach (var renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }
        }
        return bounds;
    }
}
