using UnityEngine;

namespace Roundbeargames
{
    public class MoveTransformForward : CharacterFunction
    {
        static Vector3 WorldForward = new Vector3(0f, 0f, 1f);

        public override void RunFunction(float Speed, float SpeedGraph)
        {
            if (!control.ANIMATION_DATA.IsRunning(typeof(SmoothTurn)))
            {
                control.transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);
            }
            else
            {
                if (control.TURN_DATA.StartedForward)
                {
                    if (control.GetBool(typeof(FacingForward)))
                    {
                        control.transform.position += (WorldForward * Speed * SpeedGraph * Time.deltaTime);
                    }
                }
                else
                {
                    if (!control.GetBool(typeof(FacingForward)))
                    {
                        control.transform.position -= (WorldForward * Speed * SpeedGraph * Time.deltaTime);
                    }
                }
            }
        }
    }
}