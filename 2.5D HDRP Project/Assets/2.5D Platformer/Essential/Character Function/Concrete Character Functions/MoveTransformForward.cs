using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

namespace Roundbeargames
{
    public class MoveTransformForward : CharacterFunction
    {
        public override void RunFunction(float Speed, float SpeedGraph)
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(SmoothTurn)))
            {
                control.transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
            }
            
        }
    }
}