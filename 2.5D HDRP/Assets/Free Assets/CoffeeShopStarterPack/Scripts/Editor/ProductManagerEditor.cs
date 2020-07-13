// ******------------------------------------------------------******
// ProductManagerEditor.cs
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
using System.Collections.Generic;
using System.Linq;

namespace PW
{
    [CustomEditor(typeof(ProductManager))]
    [CanEditMultipleObjects]
    public class ProductManagerEditor : Editor
    {

        SerializedProperty Products;

        SerializedProperty ProductScreenshotsPath;

        SerializedProperty placeHolderPrefab;


        ///Variables GUI to handle for foldout and scrollViewPosition reference
        ///
        bool foldOutArray;

        Vector2 scrollViewPos;
        ///
        ///
        private void OnEnable()
        {
            Products = serializedObject.FindProperty("Products");

            ProductScreenshotsPath = serializedObject.FindProperty("ProductScreenshotsPath");

            placeHolderPrefab = serializedObject.FindProperty("placeholderPrefab");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //We start drawing vertically with a scrollview

            EditorGUILayout.BeginVertical();
            scrollViewPos = EditorGUILayout.BeginScrollView(scrollViewPos);

            //Custom method to handle drawing of array of Products
            DrawPropertyArray(Products, ref foldOutArray);

            //Ending scrollview and vertical group
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();

        }

        private void DrawPropertyArray(SerializedProperty property, ref bool fold)
        {
            //Because we use size of the array several times we get a reference to it.
            SerializedProperty arraySizeProp = property.FindPropertyRelative("Array.size");

            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(placeHolderPrefab);
            //Handling foldout of the editor
            fold = EditorGUILayout.Foldout(fold, property.displayName);
            if (fold)
            {

                EditorGUILayout.PropertyField(arraySizeProp);

                EditorGUI.indentLevel++;

                //Draw invidual properties, products in our case.
                for (int i = 0; i < arraySizeProp.intValue; i++)
                {
                    EditorGUILayout.PropertyField(property.GetArrayElementAtIndex(i));
                }


                //Button to add a new product.
                if (GUILayout.Button("Add Product"))
                {
                    int curIndex = property.arraySize;
                    property.InsertArrayElementAtIndex(curIndex);
                    var entry = property.GetArrayElementAtIndex(curIndex);
                    //set the element orderID as the current ID we got.
                    if (entry != null)
                    {
                        var orderID = entry.FindPropertyRelative("orderID");
                        if (orderID != null) orderID.intValue = curIndex+1;

                    }

                    Debug.Log("Adding product");
                }
                EditorGUI.indentLevel--;


            }

            EditorGUI.indentLevel--;
            EditorGUILayout.HelpBox("Gameplay prefabs are created depending on ProductTypes, these created prefabs will contain scripts and helpers depending on their gameplay.", MessageType.Info);
            //Button to create gameplay prefabs.


            if (GUILayout.Button("Generate Gameplay Prefabs"))
            {

                GenerateGameplayPrefabs(property, arraySizeProp);
            }
            //Because we didn't want to pollute the UI folder,
            //we have set a different test folder on ScreenShotHelper.
            //But to make your process automatical, you should use the same
            //screenshot path you set in ScreenShotHelper component here;

            //If you set your screenshot path to somewwhere else, you need to point to
            //that folder instead.

            EditorGUILayout.HelpBox("This path should be the same path where you saved your screenshots with the ScreenShotHelper component in the ScreenshotScene. Whenever you add, or change a new product, you should assign screenshots below.", MessageType.Info);
            ProductScreenshotsPath.stringValue = EditorGUILayout.TextField("ScreenshotsPath", ProductScreenshotsPath.stringValue);
            EditorGUI.indentLevel++;

            //Button to find and assign screenshots to products we have.
            //OrderGenerator then, uses these screenshots to manage UI thumbnails in game.
            //ProductScreenshotspath should be same with the ScreenShotHelper class to
            //make that work. Or it should point to another folder that has your screenshots.
            if (GUILayout.Button(new GUIContent("AssignScreenShots", "This button assigns the screenshots to the products using the prefab name of products, if you changed the prefab in your screenshot, you neeed to generate screenshots again,before doing that.")))
            {

                AssignScreenShotsToOrders(serializedObject, property, arraySizeProp.intValue, ProductScreenshotsPath.stringValue);
            }
            EditorGUI.indentLevel--;

        }

        static void AssignScreenShotsToOrders(SerializedObject serializedObject, SerializedProperty property, int totalSize, string ScreenShotPath)
        {
            var orderGen = FindObjectOfType<OrderGenerator>();
            if (orderGen == null)
            {
                Debug.LogError("There is no OrderGenerator in the scnee. To assign screenshots to orders, you need to add OrderGenerator component to a gameObject in the scene.");
                return;
            }

            //Get the count of products that are included in this scene
            int countIncluded = 0;
            var pm = serializedObject.targetObject as ProductManager;
            var query = from x in pm.Products where x.dontIncludeInThisScene == false select x;
            if (query != null)
                countIncluded = query.Count();

            orderGen.orderSprites = new Sprite[countIncluded];
            //holds the orderIDs for products
            orderGen.orderedProducts = new int[countIncluded];
            int includedIndex = 0;
            for (int i = 0; i < totalSize; i++)
            {
                var curProduct = property.GetArrayElementAtIndex(i);
                if (curProduct.FindPropertyRelative("dontIncludeInThisScene").boolValue)
                {
                    continue;
                }

                //We get the screenshot name from the prefab name
                var propPrefab = curProduct.FindPropertyRelative("productPrefab");
                var propName = propPrefab.objectReferenceValue.name;

                //If object is served as a different object at the end
                //It would have a servedAsDifferentGameObject prefab assigned to it.
                //We should get it as the screenshot so, we get this prefab name here.
                var serveAsDifferent = curProduct.FindPropertyRelative("servedAsDifferentGameObject").objectReferenceValue;
                if (serveAsDifferent != null)
                    propName = serveAsDifferent.name;


                var fileName = ScreenShotPath + propName + ".png";

                if (!System.IO.File.Exists(fileName))
                {
                    Debug.LogErrorFormat("This file is not found : {0}", fileName);
                    continue;
                }

                //If everything goes well, we found the screenshot at the path,
                //We load the sprite here.
                var screenshot = AssetDatabase.LoadAssetAtPath<Sprite>(fileName);
                orderGen.orderSprites[includedIndex] = screenshot;

                //We get the orderID from the product and assign it to the array
                int orderID = curProduct.FindPropertyRelative("orderID").intValue;
                orderGen.orderedProducts[includedIndex] = orderID;

                //we first add the order and increment the index
                //so check if we are not on the last one here
                if (includedIndex < countIncluded - 1 && includedIndex < totalSize)
                    includedIndex++;
            }

            Debug.Log("Assigned the screenshots successfully.");
        }


        static void GenerateGameplayPrefabs(SerializedProperty property, SerializedProperty arraySizeProp)
        {
            string PrefabPath = "Assets/CoffeeShopStarterPack/Prefabs/Gameplay/Products/";

            if (arraySizeProp == null || arraySizeProp.intValue == 0)
                return;
            for (int i = 0; i < arraySizeProp.intValue; i++)
            {
                //Get the product from the array
                var product = property.GetArrayElementAtIndex(i);

                //Get the enum value of productType
                var serializedProductType = product.FindPropertyRelative("productType");
                int enumIndex = serializedProductType.enumValueIndex;
                ProductType pType = (ProductType)enumIndex;

                int pOrderID = product.FindPropertyRelative("orderID").intValue;

                //Get the prefab from the product
                GameObject prefab = product.FindPropertyRelative("productPrefab").objectReferenceValue as GameObject;

                if (prefab == null)
                {
                    var prodName = property.FindPropertyRelative("productName");
                    Debug.LogError(prodName + " doesn't have a prefab assigned)");
                    continue;
                }


                //Get the plate prefab to assign to the products;
                GameObject platePrefab = AssetDatabase.LoadAssetAtPath("Assets/CoffeeShopStarterPack/Prefabs/PW_plate01.prefab", typeof(GameObject)) as GameObject;
                if (platePrefab == null)
                    Debug.LogError("Plate prefab couldn't be found please check the path assigned");


                string longPrefabName = PrefabPath + prefab.name + "_type" + enumIndex + ".prefab";
                bool prefabExists = false;

                //we'll use this go to create a prefab or replace the existed one
                GameObject newPrefabGo = null;
                GameObject existedPrefab = null;
                if (!System.IO.File.Exists(longPrefabName))
                {
                    //If prefab doesnt exist we instantiate the prefab that product references.
                    newPrefabGo = Instantiate(prefab);

                }
                else
                {
                    prefabExists = true;
                    existedPrefab = AssetDatabase.LoadAssetAtPath(longPrefabName, typeof(GameObject)) as GameObject;
                    newPrefabGo = Instantiate(existedPrefab);
                    Debug.LogWarning("this prefab already exists we created an instance of it");
                }

                var boolProp = product.FindPropertyRelative("addToPlateBeforeServed");
                var serveAsDifferentProp = product.FindPropertyRelative("servedAsDifferentGameObject");

                var regenBool = product.FindPropertyRelative("RegenerateProduct");
                var plateOffset = product.FindPropertyRelative("plateOffset");

                //These gameObjects has a hierarchy of multiple meshes
                //So we just try to generate a boxcollider for this object at the root.
                //This collider will enable MouseDown events for the object.
                FindBoundsOfPrefabAndAddABoxCollider(newPrefabGo);

                //Set values specific for productTypes
                switch (pType)
                {
                    case ProductType.Cookable:

                        //Add a cookable monobehaviour to object and set it.
                        var cookable = newPrefabGo.GetComponent<CookableProduct>();
                        if (cookable==null)
                            cookable = newPrefabGo.AddComponent<CookableProduct>();
                        cookable.cookingTimeForProduct = 1f;
                        break;
                    case ProductType.Heatable:

                        //Add a heatable monobehaviour to object and set it.
                        var heatable = newPrefabGo.GetComponent<HeatableProduct>();
                        if(heatable==null)
                            heatable = newPrefabGo.AddComponent<HeatableProduct>();
                        heatable.heatingTimeForProduct = 1f;
                        break;
                    case ProductType.Drinkable:

                        var drinkable = newPrefabGo.GetComponent<DrinkableProduct>();
                        if(drinkable==null)
                            drinkable = newPrefabGo.AddComponent<DrinkableProduct>();

                        //Drinkable objects have a fillHelper for animating the filling state
                        //So lets add it .
                        var fillHelper = newPrefabGo.GetComponent<FillCupHelper>();
                        if(fillHelper==null)
                            fillHelper = newPrefabGo.AddComponent<FillCupHelper>();

                        //Every drinkable product should have an object tagged fluid.
                        //If we can find one, we use this transform to animate filling
                        for (int j = 0; j < newPrefabGo.transform.childCount; j++)
                        {
                            var child = newPrefabGo.transform.GetChild(j);
                            if (child.CompareTag("Fluid"))
                            {
                                fillHelper.fluid = child;
                                //When we found a fluid object
                                //we should scale it to make it  look empty
                                //on some objects we also transform it on -Y manually
                                child.transform.localScale = new Vector3(0, 0, 0);

                                break;
                            }
                        }
                        break;
                    case ProductType.ReadyToServe:
                        //Add a readyToServe monobehaviour to object and set it.
                        var readyToServe = newPrefabGo.GetComponent<ReadyToServe>();
                        if(readyToServe==null)
                            readyToServe = newPrefabGo.AddComponent<ReadyToServe>();
                        break;
                    case ProductType.None:
                        break;
                }

                //We're not doing this at the start
                //because we add the inherited ProductGameObject by the type of the product above

                //Set common values for base class of ProductTypes which is ProductGameObject
                ProductGameObject productMonoBehaviour = newPrefabGo.GetComponent<ProductGameObject>();
                productMonoBehaviour.orderID= pOrderID;
                productMonoBehaviour.RegenerateProduct = regenBool!=null?regenBool.boolValue:false;
                productMonoBehaviour.AddToPlateBeforeServed = boolProp != null ? boolProp.boolValue : false;
                productMonoBehaviour.plateOffset = plateOffset.vector3Value;
                productMonoBehaviour.serveAsDifferentGameObject = serveAsDifferentProp != null ? (GameObject)serveAsDifferentProp.objectReferenceValue as GameObject : null;

                if (!prefabExists)
                {
                    //Create a new prefab for gameplay purpose at the PrefabPath:
#if UNITY_2018_4_OR_NEWER
                    PrefabUtility.SaveAsPrefabAsset(newPrefabGo, longPrefabName);
#else
                    PrefabUtility.CreatePrefab(longPrefabName, newPrefabGo);
#endif
                }
                else
                {
#if UNITY_2018_4_OR_NEWER
                    PrefabUtility.SaveAsPrefabAssetAndConnect(newPrefabGo, longPrefabName, InteractionMode.AutomatedAction);
#else
                    PrefabUtility.ReplacePrefab(newPrefabGo, existedPrefab, ReplacePrefabOptions.ReplaceNameBased);
#endif

                }

                DestroyImmediate(newPrefabGo);
                
            }


        }

        static void FindBoundsOfPrefabAndAddABoxCollider(GameObject prefab)
        {
            Bounds prefab_bounds = prefab.transform.EncapsulateBounds();

            var coll = prefab.GetComponent<Collider>();
            if (coll == null)
            {
                var boxColl = prefab.AddComponent<BoxCollider>();
                boxColl.size = prefab_bounds.size;
                boxColl.center = prefab_bounds.center;
            }
            
        }
    }
}
