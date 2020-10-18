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
                    control.MOVE_DATA.Momentum += speed;
                }
            }

            if (!control.GetBool(typeof(LeftSideIsBlocked)))
            {
                if (control.MoveLeft)
                {
                    control.MOVE_DATA.Momentum -= speed;
                }
            }

            if (control.GetBool(typeof(RightSideIsBlocked)) || control.GetBool(typeof(LeftSideIsBlocked)))
            {
                float lerped = Mathf.Lerp(control.MOVE_DATA.Momentum, 0f, Time.deltaTime * 1.5f);
                control.MOVE_DATA.Momentum = lerped;
            }


            if (Mathf.Abs(control.MOVE_DATA.Momentum) >= maxMomentum)
            {
                if (control.MOVE_DATA.Momentum > 0f)
                {
                    control.MOVE_DATA.Momentum = maxMomentum;
                }
                else if (control.MOVE_DATA.Momentum < 0f)
                {
                    control.MOVE_DATA.Momentum = -maxMomentum;
                }
            }
        }
    }
}