// ******------------------------------------------------------******
// DrinkableProduct.cs
// Inherits from ProductGameObject
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

    public class DrinkableProduct : ProductGameObject
    {

        public override IEnumerator AnimateGoingToSlot()
        {
            yield return base.AnimateGoingToSlot();
            Destroy(gameObject);
        }

    }
}
