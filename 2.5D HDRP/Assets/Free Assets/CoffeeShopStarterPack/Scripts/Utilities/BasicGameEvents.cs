// ******------------------------------------------------------******
// BasicGameEvents.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;
namespace PW {
    public class BasicGameEvents : MonoBehaviour
    {

        public GameObject placeholderPrefab;

        #region orderCancelledEvent

        public delegate void OnOrderCancelled(int ID);

        public static event OnOrderCancelled onOrderCancelled;

        public static void RaiseOnOrderCancelled(int ID)
        {
            if (onOrderCancelled != null)
            {
                onOrderCancelled.Invoke(ID);
            }
        }
        #endregion

        #region orderCompletedEvent

        public delegate void OnOrderCompleted(int ID,float percentageSuccess);

        public static event OnOrderCompleted onOrderCompleted;

        public static void RaiseOnOrderCompleted(int ID,float percentageSuccess)
        {
            if (onOrderCompleted != null)
            {
                onOrderCompleted.Invoke(ID,percentageSuccess);
            }
        }
        #endregion

        public delegate void OnProductAddedToSlot(int ID);

        public static event OnProductAddedToSlot onProductAddedToSlot;

        public static void RaiseOnProductAddedToSlot(int ID)
        {
            if (onProductAddedToSlot != null)
            {
                onProductAddedToSlot.Invoke(ID);
            }
        }

        public delegate void OnProductDeletedFromSlot(int ID);

        public static event OnProductDeletedFromSlot onProductDeletedFromSlot;

        public static void RaiseOnProductDeletedFromSlot(int ID)
        {
            if (onProductDeletedFromSlot != null)
            {
                onProductDeletedFromSlot.Invoke(ID);
            }
        }

        public delegate void OnPlaceHolderRequired(Transform parent,Vector3 pos, GameObject go);

        public static event OnPlaceHolderRequired onPlaceHolderRequired;
        public static void RaiseInstantiatePlaceHolder(Transform parent,Vector3 pos,GameObject go)
        {
            if (onPlaceHolderRequired != null)
                onPlaceHolderRequired.Invoke(parent,pos,go);
            
        }

    }
}
