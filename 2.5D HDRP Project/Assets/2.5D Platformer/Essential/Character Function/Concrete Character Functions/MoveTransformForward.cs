using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class MoveTransformForward : CharacterFunction
    {
        public override void RunFunction(float Speed, float SpeedGraph)
        {
            control.transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
        }
    }
}