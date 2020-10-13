// ******------------------------------------------------------******
// OrderGenerator.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PW
{
    public class OrderGenerator : MonoBehaviour
    {
        //This limits generating orders constantly
        public int MaxConcurrentOrder=4;

        public int currentOrderCount;

        public Sprite[] orderSprites;

        [HideInInspector]
        public int[] orderedProducts;

        public Transform UIParentForOrders;

        public GameObject orderRepPrefab;//The general prefab for order represantation

        private void OnEnable()
        {
            //We'll listen for order events;
            BasicGameEvents.onOrderCancelled += BasicGameEvents_onOrderCancelled;
            BasicGameEvents.onOrderCompleted += BasicGameEvents_onOrderCompleted;

        }
        private void OnDisable()
        {
            //Don't forget to remove listeners from events on disable.
            BasicGameEvents.onOrderCancelled -= BasicGameEvents_onOrderCancelled;
            BasicGameEvents.onOrderCompleted -= BasicGameEvents_onOrderCompleted;

        }

        private void BasicGameEvents_onOrderCancelled(int ID)
        {
            //We could also do something with the ID of the product,
            //Or we could pass other things as parameters,
            //but for demo purposes this is fine.
            currentOrderCount--;

        }

        private void BasicGameEvents_onOrderCompleted(int ID,float percentageSucccess)
        {
            currentOrderCount--;
            //In a common gameplay logic,
            //We would add money, play effects, maybe check our list of products to complete here,
            //by raising an another event or calling a method of a gamemanager like script.
            //i.e. GameManager.CheckMilestonesForOrderID(ID)
            //or BasicGameEvents.onMoneyIncreased(ID,percentageSuccess)
            //percentage of Success can define the xp we got, or money multiplier and so forth.


            //You could also use another float as a third parameter to check if an order is overcooked,
            //or just perfect.
            //You could also check combo multipliers for multiple fast deliveries
        }


        void Start()
        {
            //In a demo only manner we start calling the coroutine here on Start.

            StartCoroutine(GenerateOrderRoutine(3f));

        }


        public IEnumerator GenerateOrderRoutine(float intervalTime)
        {
            //We assume we don't pause the game or something,
            //You should check if your game state is playing here
            while (true)
            {
                if (currentOrderCount < MaxConcurrentOrder)
                {
                    GenerateOrder();
                    yield return new WaitForSeconds(intervalTime);
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        public void GenerateOrder()
        {
            Debug.Log("Generating order");

            //Get a random ID from sprites list
            //We could store the ID of the object to track last generated orders,
            //Totally random generation may create the same order in row repeatedly.

            int spriteIndex = Random.Range(0, orderSprites.Length);

            int orderID = orderedProducts[spriteIndex];

            var newOrder = GameObject.Instantiate(orderRepPrefab, UIParentForOrders).GetComponent<ServeOrder>();

            newOrder.SetOrder(orderID,Random.Range(5f,40f));

            newOrder.SetSprite(orderSprites[spriteIndex]);

            currentOrderCount++;

        }

        public Sprite GetSpriteForOrder(int orderID)
        {
            var spriteIndex = Array.IndexOf(orderedProducts, orderID);
            if (spriteIndex<0)
                return null;
            return orderSprites[spriteIndex];
        }
    }
}