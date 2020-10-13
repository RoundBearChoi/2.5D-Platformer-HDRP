// ******------------------------------------------------------******
// PlayerSlots.cs
//
// PlayerSlots is some kind of a inventory mechanism. 
// When player has a product or thing in their hands, we add the item to slots.
//
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace PW
{
    public class PlayerSlots : MonoBehaviour
    {
        public int slotCount;

        //Player holds order ID's of items in those slots;
        int[] slotItems;

        public Image[] slotUIObjects;

        private void OnEnable()
        {
            if (slotItems == null)
                slotItems = new int[3] { -1, -1, -1 };

            BasicGameEvents.onProductAddedToSlot += BasicGameEvents_onProductAddedToSlot;
            BasicGameEvents.onProductDeletedFromSlot += BasicGameEvents_onProductDeletedFromSlot;
        }

        private void BasicGameEvents_onProductDeletedFromSlot(int ID)
        {
            if(slotItems!=null && slotItems.Length>0)
                slotItems[ID]=-1;
        }

        private void BasicGameEvents_onProductAddedToSlot(int orderID)
        {
            var orderGenerator = FindObjectOfType<OrderGenerator>();
            
                //find the first empty index
                var emptyIndex = Array.IndexOf(slotItems, -1);
                slotItems[emptyIndex]=orderID;
                slotUIObjects[emptyIndex].sprite = orderGenerator.GetSpriteForOrder(orderID);
            StartCoroutine(DoEmphasize(emptyIndex));

        }

        public IEnumerator DoEmphasize(int index)
        {
            //You can do a better version of this with DOTween punchscale;
            var uiImage = slotUIObjects[index];
            var outline = uiImage.GetComponent<Outline>();
            Color outlineColor = outline.effectColor;
            float totalTime = .6f;
            float curTime = totalTime;
            var alphaVal = 1f;
            while (curTime > 0)
            {
                curTime -= Time.deltaTime;

                uiImage.transform.localScale += Vector3.one * 0.1f * -1f * Mathf.Sin(totalTime - 2 * curTime);
                //animate outline alpha
                alphaVal+= 0.1f * -1f * Mathf.Sin(totalTime - 2 * curTime);
                outline.effectColor = new Color(outlineColor.r, outlineColor.g, outlineColor.b, alphaVal);
                yield return null;
            }
            uiImage.transform.localScale = Vector3.one;
            outline.effectColor = new Color(outlineColor.r,outlineColor.g,outlineColor.b,0f);

        }

        private void OnDisable()
        {
            BasicGameEvents.onProductAddedToSlot -= BasicGameEvents_onProductAddedToSlot;
            BasicGameEvents.onProductDeletedFromSlot -= BasicGameEvents_onProductDeletedFromSlot;

        }


        public bool CanHoldItem(int orderID)
        {
            //you can also check for orderID here such
            //maybe you don't want to let player hold the same order more than once.
            var emptyIndex = Array.IndexOf(slotItems, -1);

            return emptyIndex >= 0;
        }

        public bool HoldsItem(int orderID)
        {
            int indexofOrder = Array.IndexOf(slotItems, orderID);
            if (indexofOrder == -1)
            {
                //we don't have an item with such orderID
                return false;
            }

            //remove the UI image
            slotUIObjects[indexofOrder].sprite = null;

            //Remove the slot, we just served.
            slotItems[indexofOrder] = -1;
            
            return true;
        }

        
    }
}