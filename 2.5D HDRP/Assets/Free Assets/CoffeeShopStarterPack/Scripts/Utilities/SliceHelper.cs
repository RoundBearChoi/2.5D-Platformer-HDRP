// ******------------------------------------------------------******
// SliceHelper.cs
// The job of slice placing is done in the editor script. SliceHelperEditor
// This script mainly exist to show a way of how to help arrange
// sliced items i.e. cheesecakes
// You can remove this script component after you're happy with the results.
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
	public class SliceHelper : MonoBehaviour
	{
        [SerializeField]
		public GameObject slicePrefab;
        [SerializeField]
        public int sliceCount;
	}
}