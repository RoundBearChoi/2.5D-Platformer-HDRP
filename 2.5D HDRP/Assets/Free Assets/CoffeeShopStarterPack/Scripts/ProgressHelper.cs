// ******------------------------------------------------------******
// ProgressHelper.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PW
{
    public class ProgressHelper : MonoBehaviour
    {
	    public Image m_Image;

        
        public void UpdateProcessUI(float curAmount,float totalProcess)
	    {
		    if (m_Image != null)
				m_Image.fillAmount = curAmount / totalProcess;

	    }

        public void ToggleHelper(bool result)
        {
            gameObject.SetActive(result);

			m_Image.fillAmount = 0;
        }


    }
}
