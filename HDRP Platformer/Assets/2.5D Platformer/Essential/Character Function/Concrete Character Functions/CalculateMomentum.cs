using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CalculateMomentum : CharacterFunction
    {
        public override void RunFunction(float speed, float maxMomentum)
        {
            if (!control.GetBool(typeof(RightSideIsBlocked)))
            {
                if (control.MoveRight)
                {
                    control.MOMENTUM_DATA.Momentum += speed;
                }
            }

            if (!control.GetBool(typeof(LeftSideIsBlocked)))
            {
                if (control.MoveLeft)
                {
                    control.MOMENTUM_DATA.Momentum -= speed;
                }
            }

            if (control.GetBool(typeof(RightSideIsBlocked)) || control.GetBool(typeof(LeftSideIsBlocked)))
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