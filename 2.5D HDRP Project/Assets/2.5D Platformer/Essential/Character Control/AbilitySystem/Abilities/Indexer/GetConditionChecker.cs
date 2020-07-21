using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public static class GetConditionChecker
    {
        static Dictionary<TransitionConditionType, CheckCondition> DicCheckers;
        static GameObject Conditions = null;

        public static CheckCondition GET(TransitionConditionType conditionType)
        {
            if (DicCheckers == null)
            {
                InitDic();
            }
            
            return DicCheckers[conditionType];
        }

        public static void InitDic()
        {
            DicCheckers = new Dictionary<TransitionConditionType, CheckCondition>();

            _Add(TransitionConditionType.UP, typeof(ConditionCheck_Up));
            _Add(TransitionConditionType.DOWN, typeof(ConditionCheck_Down));
            _Add(TransitionConditionType.LEFT, typeof(ConditionCheck_Left));
            _Add(TransitionConditionType.RIGHT, typeof(ConditionCheck_Right));
            _Add(TransitionConditionType.ATTACK, typeof(ConditionCheck_Attack));
            _Add(TransitionConditionType.JUMP, typeof(ConditionCheck_Jump));
            _Add(TransitionConditionType.GRABBING_LEDGE, typeof(ConditionCheck_GrabbingLedge));
            _Add(TransitionConditionType.NOT_GRABBING_LEDGE, typeof(ConditionCheck_GrabbingLedge_NOT));
            _Add(TransitionConditionType.LEFT_OR_RIGHT, typeof(ConditionCheck_Left_or_Right));
            _Add(TransitionConditionType.GROUNDED, typeof(ConditionCheck_Grounded));
            _Add(TransitionConditionType.NOT_GROUNDED, typeof(ConditionCheck_Grounded_NOT));
            _Add(TransitionConditionType.MOVE_FORWARD, typeof(ConditionCheck_MoveForward));
            _Add(TransitionConditionType.BLOCKED_BY_WALL, typeof(ConditionCheck_BlockedByWall));
            _Add(TransitionConditionType.NOT_BLOCKED_BY_WALL, typeof(ConditionCheck_BlockedByWall_NOT));
            _Add(TransitionConditionType.CAN_WALLJUMP, typeof(ConditionCheck_CanWallJump));
            _Add(TransitionConditionType.MOVING_TO_BLOCKING_OBJ, typeof(ConditionCheck_MovingToBlockingObj));
            _Add(TransitionConditionType.DOUBLE_TAP_UP, typeof(ConditionCheck_DoubleTap_Up));
            _Add(TransitionConditionType.DOUBLE_TAP_DOWN, typeof(ConditionCheck_DoubleTap_Down));
            _Add(TransitionConditionType.DOUBLE_TAP_LEFT, typeof(ConditionCheck_DoubleTap_Left));
            _Add(TransitionConditionType.DOUBLE_TAP_RIGHT, typeof(ConditionCheck_DoubleTap_Right));
            _Add(TransitionConditionType.TOUCHING_WEAPON, typeof(ConditionCheck_TouchingWeapon));
            _Add(TransitionConditionType.HOLDING_AXE, typeof(ConditionCheck_HoldingAxe));
            _Add(TransitionConditionType.NOT_MOVING, typeof(ConditionCheck_Moving_NOT));
            _Add(TransitionConditionType.RUN, typeof(ConditionCheck_Running));
            _Add(TransitionConditionType.NOT_RUNNING, typeof(ConditionCheck_Running_NOT));
            _Add(TransitionConditionType.BLOCKING, typeof(ConditionCheck_Blocking));
            _Add(TransitionConditionType.NOT_BLOCKING, typeof(ConditionCheck_Blocking_NOT));
            _Add(TransitionConditionType.ATTACK_IS_BLOCKED, typeof(ConditionCheck_AttackIsBlocked));
            _Add(TransitionConditionType.NOT_TURBO, typeof(ConditionCheck_Turbo_NOT));
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

            if (CheckConditionType.IsSubclassOf(typeof(CheckCondition)))
            {
                GameObject obj = new GameObject();
                obj.transform.parent = Conditions.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
                obj.name = CheckConditionType.ToString();
                obj.name = obj.name.Replace("Roundbeargames.", "");

                CheckCondition c = obj.AddComponent(CheckConditionType) as CheckCondition;
                DicCheckers.Add(transitionConditionType, c);
            }
        }
    }
}