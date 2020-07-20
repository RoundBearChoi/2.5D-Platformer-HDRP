using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class MomentumCalculator : CharacterUpdate
    {
        public override void InitComponent()
        {
            control.MOMENTUM_DATA.CalculateMomentum = CalculateMomentum;
        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }

        void CalculateMomentum(float speed, float maxMomentum)
        {
            if (!control.BLOCKING_DATA.RightSideBlocked())
            {
                if (control.MoveRight)
                {
                    control.MOMENTUM_DATA.Momentum += speed;
                }
            }

            if (!control.BLOCKING_DATA.LeftSideBlocked())
            {
                if (control.MoveLeft)
                {
                    control.MOMENTUM_DATA.Momentum -= speed;
                }
            }

            if (control.BLOCKING_DATA.RightSideBlocked() || control.BLOCKING_DATA.LeftSideBlocked())
            {
                float lerped = Mathf.Lerp(control.MOMENTUM_DATA.Momentum, 0f, Time.deltaTime * 1.5f);
                control.MOMENTUM_DATA.Momentum = lerped;
            }


            if (Mathf.Abs(control.MOMENTUM_DATA.Momentum) >= maxMomentum)
            {
                if (control.MOMENTUM_DATA.Momentum > 0f)
                {
                    control.MOMENTUM_DATA.Momentum = maxMomentum;
                }
                else if (control.MOMENTUM_DATA.Momentum < 0f)
                {
                    control.MOMENTUM_DATA.Momentum = -maxMomentum;
                }
            }
        }
    }
}