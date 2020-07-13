// 
// ReadyToServe.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PW
{
    public class ReadyToServe : ProductGameObject
    {

        public GameObject platePrefab;

        Collider m_collider;

        private void Awake()
        {
            m_collider = GetComponent<Collider>();
            m_collider.enabled = true;
        }

        void OnMouseDown()
        {
            if (!base.CanGoPlayerSlot())
            {
                return;
            }

            if (AddToPlateBeforeServed)
            {
                var plate = GameObject.Instantiate(platePrefab, transform.position, Quaternion.identity);
                plate.transform.SetParent(transform);
                if (plateOffset.magnitude > 0)
                {
                    plate.transform.localPosition = plateOffset;
                }
                plate.transform.SetAsFirstSibling();//so we know what to delete later

            }
            if (RegenerateProduct)
            {
                BasicGameEvents.RaiseInstantiatePlaceHolder(transform.parent, transform.position, gameObject);
            }
            StartCoroutine(AnimateGoingToSlot());

        }

        public override IEnumerator AnimateGoingToSlot()
        {

            yield return base.AnimateGoingToSlot();

            gameObject.SetActive(false);
        }

    }
}