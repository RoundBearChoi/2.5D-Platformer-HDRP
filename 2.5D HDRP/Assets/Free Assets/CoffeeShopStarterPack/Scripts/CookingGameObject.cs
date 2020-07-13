// ******------------------------------------------------------******
// CookingGameObject.cs
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
    public class CookingGameObject : MonoBehaviour
    {

        //offset to Pivot, Vector3.zero is the default.
        public Vector3 cookingSpot;

        //Animation starting position can be set here,zero is default.
        public Vector3 startingPositionOffset;

        public CookableProduct currentProduct;

        public GameObject progressHelperprefab;

        [HideInInspector]
        public ProgressHelper m_progressHelper;

        public float doorAnimTime = 1f;

        float cookingProcess;

        Collider m_Collider;

        private void Awake()
        {
            m_Collider = GetComponent<Collider>();
        }

        private void Start()
        {
            //Instantiate and set the UI indicator
            if (progressHelperprefab != null)
            {
                m_progressHelper = Instantiate(progressHelperprefab, transform).GetComponent<ProgressHelper>();
                //dont show the indicator now
                m_progressHelper.ToggleHelper(false);
            }
        }
        /// <summary>
        /// We use this method to make this available through a system,
        /// if you had more than one cooking object and a manager had selected one of the pans,
        /// you would get this script from the array and just call this method
        /// to get the exact position of the available cooking object.
        /// </summary>
        /// <returns>Cooking position of the object in WorldPos with offset</returns>
        public virtual Vector3 GetCookingPosition()
        {
            return transform.position + cookingSpot;
        }

        public virtual void DoDoorAnimationsIfNeeded()
        {
           
        }

        public virtual bool IsEmpty()
        {
            return currentProduct == null;
        }

        public virtual void StartCooking(CookableProduct product)
        {
            currentProduct = product;
            cookingProcess = product.cookingTimeForProduct;

            m_Collider.enabled = false;
            StartCoroutine(Cooking());
        }

        public virtual void ReadyToServe()
        {
            currentProduct = null;
            m_Collider.enabled = true;
            DoDoorAnimationsIfNeeded();
        }

        public virtual bool HasStartAnimationPos(out Vector3 result)
        {
            result = Vector3.zero;
            if (startingPositionOffset != Vector3.zero)
            {
                result = transform.position + startingPositionOffset;
                return true;
            }
            else
                return false;
        }

        public virtual IEnumerator Cooking()
        {
            m_progressHelper.ToggleHelper(true);
            var curTime = cookingProcess+doorAnimTime;
            while (curTime > 0)
            {
                curTime -= Time.deltaTime;
                m_progressHelper.UpdateProcessUI(curTime, cookingProcess);
                yield return null;
            }
            currentProduct.DoneCooking();
            m_progressHelper.ToggleHelper(false);
            m_Collider.enabled = true;
        }

        public virtual void OnMouseDown()
        {
            if(currentProduct!=null && currentProduct.IsCooked)
            {
                //Try to serve currentProduct if player slots are available
                if(currentProduct.CanGoPlayerSlot())
                    ReadyToServe();
            }
            else
            {
                    DoDoorAnimationsIfNeeded();
            }

        }
    }
}