// ******------------------------------------------------------******
// StoveGameObject.cs
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

    public class StoveGameObject : CookingGameObject
    {

        public Transform doorTransform;

        private Vector3 progressHelperOffset = new Vector3(0f, 0.8f, 0f);

        private bool doorIsOpen = false;
        private bool isAnimating;

        public override void DoDoorAnimationsIfNeeded()
        {
            base.DoDoorAnimationsIfNeeded();
            if(!isAnimating)
                StartCoroutine(PlayDoorAnim(true, true));
        }

        public override void StartCooking(CookableProduct product)
        {
            base.StartCooking(product);
            m_progressHelper.transform.position +=progressHelperOffset;
        }

        IEnumerator PlayDoorAnim(bool open, bool alsoReverse = false)
        {
            doorIsOpen = open;
            isAnimating = true;
            float totalTime = doorAnimTime;
            float curTime = totalTime;
            float totalAngle = 66;
            float multiplier = 1f;
            float finalAngle = 66;
            if (!open)
            {
                finalAngle = 0;
                multiplier = -1f;
            }

            while (curTime > 0)
            {
                var amount = Time.deltaTime;
                var eulerTemp = doorTransform.rotation.eulerAngles;

                doorTransform.Rotate(new Vector3( (multiplier * totalAngle) * amount / totalTime,0f, 0f),Space.Self);
                curTime -= Time.deltaTime;
                yield return null;
            }
            doorTransform.localRotation= Quaternion.Euler(new Vector3(finalAngle,0f, 0f));
            doorIsOpen = false;

            yield return new WaitForSeconds(.2f);
            if (alsoReverse)
            {
                yield return StartCoroutine(PlayDoorAnim(!open, false));
                isAnimating = false;

            }
            else
            isAnimating = false;
        }

        public override void OnMouseDown()
        {
            base.OnMouseDown();
        }

    }
}
