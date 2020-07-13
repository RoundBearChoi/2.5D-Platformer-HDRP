// ******------------------------------------------------------******
// OnClickCoverHelper.cs
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
using UnityEngine.Events;

namespace PW
{
    [RequireComponent(typeof(Collider))]
    public class OnClickCoverHelper : MonoBehaviour {
        [SerializeField]
        public UnityEvent methodToCall;
        Collider m_collider;
        void OnEnable()
        {
            m_collider = GetComponent<Collider>();
            m_collider.enabled = false;
        }
        private void OnMouseDown()
        {

            if (methodToCall != null)
            {

                methodToCall.Invoke();
                m_collider.enabled = false;
            }
        }

        public void ActivateCollider()
        {
            m_collider.enabled = true;
        }
    }
}
