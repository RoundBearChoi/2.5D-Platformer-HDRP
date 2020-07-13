// ******------------------------------------------------------******
// Product.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;

namespace PW
{
    [System.Serializable]
    public class Product
    {
        [SerializeField]
        public GameObject productPrefab;

        [SerializeField]
        public int orderID=0;

        [SerializeField]
        public string productName;

        [SerializeField]
        public float productPrice=10;

        [SerializeField]
        public ProductType productType;

        [SerializeField]
        public bool addToPlateBeforeServed = true;

        [SerializeField]
        public bool dontIncludeInThisScene = false;

        [SerializeField]
        public GameObject servedAsDifferentGameObject;

        [SerializeField]
        public Vector3 plateOffset;

        [SerializeField]
        public bool RegenerateProduct;


        public Product(int ID,string name,ProductType _type, float price)
        {
            orderID = ID;

            productName = name;

            productType = _type;

            productPrice = price;

            dontIncludeInThisScene = false;

            plateOffset = Vector3.zero;

            RegenerateProduct = false;

        }

    }                                                                                                                                                                           
}