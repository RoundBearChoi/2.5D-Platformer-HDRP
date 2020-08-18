using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class GetConditionChecker
    {
        static Dictionary<TransitionConditionType, CheckConditionBase> DicCheckers;
        static GameObject Conditions = null;

        public static CheckConditionBase GET(TransitionConditionType conditionType)
        {
            if (DicCheckers == null)
            {
                InitDic();
            }
            
            return DicCheckers[conditionType];
        }

        public static void InitDic()
        {
            DicCheckers = new Dictionary<TransitionConditionType, CheckConditionBase>();

            _Add(TransitionConditionType.UP, typeof(ConditionCheck_Up));
            _Add(TransitionConditionType.DOWN, typeof(ConditionCheck_Down));
            _Add(TransitionConditionType.LEFT, typeof(ConditionCheck_Left));
            _Add(TransitionConditionType.RIGHT, typeof(ConditionCheck_Right));
            _Add(TransitionConditionType.ATTACK, typeof(ConditionCheck_Attack));
            _Add(TransitionConditionType.JUMP, typeof(ConditionCheck_Jump));
            _Add(TransitionConditionType.GRABBING_LEDGE, typeof(ConditionCheck_GrabbingLedge));
            _Add(TransitionConditionType.LEFT_OR_RIGHT, typeof(ConditionCheck_Left_or_Right));
            _Add(TransitionConditionType.GROUNDED, typeof(ConditionCheck_Grounded));
            _Add(TransitionConditionType.MOVE_FORWARD, typeof(ConditionCheck_MoveForward));
            _Add(TransitionConditionType.MOVE_BACK, typeof(ConditionCheck_MoveBack));
            _Add(TransitionConditionType.BLOCKED_BY_WALL, typeof(ConditionCheck_BlockedByWall));
            _Add(TransitionConditionType.CAN_WALLJUMP, typeof(ConditionCheck_CanWallJump));
            _Add(TransitionConditionType.MOVING_TO_BLOCKING_OBJ, typeof(ConditionCheck_MovingToBlockingObj));
            _Add(TransitionConditionType.DOUBLE_TAP_UP, typeof(ConditionCheck_DoubleTap_Up));
            _Add(TransitionConditionType.DOUBLE_TAP_DOWN, typeof(ConditionCheck_DoubleTap_Down));
            _Add(TransitionConditionType.DOUBLE_TAP_LEFT, typeof(ConditionCheck_DoubleTap_Left));
            _Add(TransitionConditionType.DOUBLE_TAP_RIGHT, typeof(ConditionCheck_DoubleTap_Right));
            _Add(TransitionConditionType.TOUCHING_WEAPON, typeof(ConditionCheck_TouchingWeapon));
            _Add(TransitionConditionType.HOLDING_AXE, typeof(ConditionCheck_HoldingAxe));
            _Add(TransitionConditionType.MOVING, typeof(ConditionCheck_Moving));
            _Add(TransitionConditionType.TURBO, typeof(ConditionCheck_Turbo));
            _Add(TransitionConditionType.RUN, typeof(ConditionCheck_Running));
            _Add(TransitionConditionType.BLOCKING, typeof(ConditionCheck_Blocking));
            _Add(TransitionConditionType.ATTACK_IS_BLOCKED, typeof(ConditionCheck_AttackIsBlocked));
        }

        static void _Add(TransitionConditionType transitionConditionType, System.Type CheckConditionType)
        {
            if (Conditions == null)
            {
                Conditions = new GameObject();
                Conditions.name = "Condition Checkers";
                Conditions.transform.position = Vector3.zero;
                Conditions.transform.rotation = Quaternion.identity;
            }

            if (CheckConditionType.IsSubclassOf(typeof(CheckConditionBase)))
            {
                GameObject obj = new GameObject();
                obj.transform.parent = Conditions.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.name = CheckConditionType.ToString();
                obj.name = obj.name.Replace("Roundbeargames.", "");

                CheckConditionBase c = obj.AddComponent(CheckConditionType) as CheckConditionBase;
                DicCheckers.Add(transitionConditionType, c);
            }
        }
    }
}