using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ManualInput : CharacterUpdate
    {
        List<InputKeyType> UpKeys = new List<InputKeyType>();
        Dictionary<InputKeyType, float> DicDoubleTapTimings = new Dictionary<InputKeyType, float>();

        public ManualInputData INPUT_DATA
        {
            get
            {
                return control.MANUAL_INPUT_DATA;
            }
        }

        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            if (VirtualInputManager.Instance.Turbo)
            {
                control.Turbo = true;
                ProcDoubleTap(InputKeyType.KEY_TURBO);
            }
            else
            {
                control.Turbo = false;
                RemoveDoubleTap(InputKeyType.KEY_TURBO);
            }

            if (VirtualInputManager.Instance.MoveUp)
            {
                control.MoveUp = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_UP);
            }
            else
            {
                control.MoveUp = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_UP);
            }

            if (VirtualInputManager.Instance.MoveDown)
            {
                control.MoveDown = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_DOWN);
            }
            else
            {
                control.MoveDown = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_DOWN);
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                control.MoveRight = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
            }
            else
            {
                control.MoveRight = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_RIGHT);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                control.MoveLeft = true;
                ProcDoubleTap(InputKeyType.KEY_MOVE_LEFT);
            }
            else
            {
                control.MoveLeft = false;
                RemoveDoubleTap(InputKeyType.KEY_MOVE_LEFT);
            }

            if (VirtualInputManager.Instance.Jump)
            {
                control.Jump = true;
                ProcDoubleTap(InputKeyType.KEY_JUMP);
            }
            else
            {
                control.Jump = false;
                RemoveDoubleTap(InputKeyType.KEY_JUMP);
            }

            if (VirtualInputManager.Instance.Block)
            {
                control.Block = true;
                ProcDoubleTap(InputKeyType.KEY_BLOCK);
            }
            else
            {
                control.Block = false;
                RemoveDoubleTap(InputKeyType.KEY_BLOCK);
            }

            if (VirtualInputManager.Instance.Attack)
            {
                control.Attack = true;
                ProcDoubleTap(InputKeyType.KEY_ATTACK);
            }
            else
            {
                control.Attack = false;
                RemoveDoubleTap(InputKeyType.KEY_ATTACK);
            }

            //double tap running
            if (INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT) ||
                INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
            {
                control.Turbo = true;
            }

            //double tap running turn
            if (control.MoveRight && control.MoveLeft)
            {
                if (INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT) ||
                    INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
                {
                    if (!INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_RIGHT))
                    {
                        INPUT_DATA.DoubleTaps.Add(InputKeyType.KEY_MOVE_RIGHT);
                    }

                    if (!INPUT_DATA.DoubleTaps.Contains(InputKeyType.KEY_MOVE_LEFT))
                    {
                        INPUT_DATA.DoubleTaps.Add(InputKeyType.KEY_MOVE_LEFT);
                    }
                }
            }
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }

        void ProcDoubleTap(InputKeyType keyType)
        {
            if (!DicDoubleTapTimings.ContainsKey(keyType))
            {
                DicDoubleTapTimings.Add(keyType, 0f);
            }

            if (DicDoubleTapTimings[keyType] == 0f ||
                UpKeys.Contains(keyType))
            {
                if (Time.time < DicDoubleTapTimings[keyType])
                {
                    if (!INPUT_DATA.DoubleTaps.Contains(keyType))
                    {
                        INPUT_DATA.DoubleTaps.Add(keyType);
                    }
                }

                if (UpKeys.Contains(keyType))
                {
                    UpKeys.Remove(keyType);
                }
                
                DicDoubleTapTimings[keyType] = Time.time + 0.18f;
            }
        }

        void RemoveDoubleTap(InputKeyType keyType)
        {
            if (INPUT_DATA.DoubleTaps.Contains(keyType))
            {
                INPUT_DATA.DoubleTaps.Remove(keyType);
            }

            if (!UpKeys.Contains(keyType))
            {
                UpKeys.Add(keyType);
            }
        }
    }
}