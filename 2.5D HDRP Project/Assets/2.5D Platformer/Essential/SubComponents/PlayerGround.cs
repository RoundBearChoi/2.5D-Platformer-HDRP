using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerGround : SubComponent
    {
        public GroundData groundData;

        private void Start()
        {
            groundData = new GroundData
            {

            };

            control.characterData.groundData = groundData;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}