// ******------------------------------------------------------******
// ProductManager.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEditor;

namespace PW
{
    public class ProductManager : MonoBehaviour
    {
        /// <summary>
        /// Array of products that would be used in game.
        /// You can add new ones in the editor inspector of this script.
        /// see ProductManagerEditor script for how it handles and change it as you like.
        /// </summary>
        [SerializeField]
        public Product[] Products;

        /// <summary>
        /// This should point to the folder you set in the screenshot helper
        /// If you didn't change it for your own purposes this would work with default value.
        /// <see cref="ScreenShotHelper.ScreenShotPath"/>
        /// </summary>
        [SerializeField]
        public string ProductScreenshotsPath;

        [SerializeField]
        public GameObject placeholderPrefab;

        public void InstantiatePlaceHolder(Transform parent,Vector3 pos,GameObject go)
        {
            if (placeholderPrefab != null)
            {
                var placeGo = Instantiate(placeholderPrefab, pos, Quaternion.identity,parent);
                placeGo.GetComponent<CreateProductOnPlaceHolder>().objectToGenerate = go;
            }
            else
            {
                Debug.LogError("You are using this feature on the products but you didn't assign a prefab");
            }

        }

        private void OnEnable()
        {
            BasicGameEvents.onPlaceHolderRequired += InstantiatePlaceHolder;
        }

        private void OnDisable()
        {
            BasicGameEvents.onPlaceHolderRequired -= InstantiatePlaceHolder;

        }

        [ExecuteInEditMode]
        public bool RemoveProduct(int orderID)
        {
            var query = from x in Products where x.orderID == orderID select x ;

            Debug.Log("Trying to remove product with ID : " + orderID);

            if (query.Count() > 0)
            {
                var FoundProduct = query.First();
                var indexOfProduct = Array.IndexOf(Products,FoundProduct);

                //Delete the found product
                Products[indexOfProduct] = null;

                //Rearange our product array
                var tempArr = new Product[Products.Length -1];
                var newIndex = 0;
                for (int i = 0; i < Products.Length; i++)
                {
                    if (Products[i] != null)
                    {
                        tempArr[newIndex] = Products[i];

                        if (newIndex < Products.Length-2)
                            newIndex++;
                    }
                }
                Products = new Product[tempArr.Length];
                Array.Copy(tempArr, Products, tempArr.Length);
                return true;
            }
            else
            {
                Debug.Log("But couldn't found one matchinng");
                return false;
            }

        }

    }

}
