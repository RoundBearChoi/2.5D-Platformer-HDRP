// ******------------------------------------------------------******
// ProductWithCover.cs
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
namespace PW
{

    public class ProductWithCover : MonoBehaviour {

        Collider m_collider;

        public Transform coverObject;

        public Vector3 openCoverOffset;

        public bool autoCloseCover;

        const float k_AutoCloseCoverTime = 0.5f;

        bool IsAnimating = false;

        void OnEnable() {
            m_collider = GetComponent<Collider>();
        }


        public void HandleCoverCloseClick()
        {
            if (IsAnimating)
                return;
            StartCoroutine(OpenCloseDisplay(false, false));
        }


        private void OnMouseDown()
        {
            if (IsAnimating)
                return;

            //Open the cover
            StartCoroutine(OpenCloseDisplay(true, autoCloseCover));
        }
        IEnumerator OpenCloseDisplay(bool open, bool alsoReverse = false)
        {
            IsAnimating = true;
            float totalTime = 1f;
            float curTime = totalTime;
            var totalDist = (openCoverOffset);
            var finalPos = coverObject.position + openCoverOffset;

            if (!open)
            {
                totalDist = -openCoverOffset;
                finalPos = coverObject.position - openCoverOffset;
            }

            while (curTime > 0)
            {
                var amount = Time.deltaTime;
                var eulerTemp = coverObject.transform.rotation.eulerAngles;

                coverObject.transform.position += (totalDist * amount) / totalTime;
                curTime -= Time.deltaTime;
                yield return null;
            }
            m_collider.enabled = !open;

            coverObject.transform.position = finalPos;
            
            yield return new WaitForSeconds(.2f);

            if (alsoReverse)
            {
                if (autoCloseCover)
                {
                    //If auto closing enable wait for relevant time before closing.
                    yield return new WaitForSeconds(k_AutoCloseCoverTime);
                }
                yield return StartCoroutine(OpenCloseDisplay(!open, false));
                yield break;
            }

            m_collider.enabled = !open;

            if (open)
                coverObject.GetComponent<OnClickCoverHelper>().ActivateCollider();
            IsAnimating = false;

        }

    }
}