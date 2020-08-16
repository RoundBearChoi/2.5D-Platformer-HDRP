using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum TransitionConditionType
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        JUMP = 5,
        LEFT_OR_RIGHT = 7,
        MOVE_FORWARD = 9,
        RUN = 23,

        MOVING = 100,
        TURBO = 200,

        DOUBLE_TAP_UP = 16,
        DOUBLE_TAP_DOWN = 17,
        DOUBLE_TAP_LEFT = 18,
        DOUBLE_TAP_RIGHT = 19,

        ATTACK = 4,

        GROUNDED = 8,

        GRABBING_LEDGE = 6,

        BLOCKED_BY_WALL = 11,
        CAN_WALLJUMP = 12,

        MOVING_TO_BLOCKING_OBJ = 15,

        TOUCHING_WEAPON = 20,
        HOLDING_AXE = 21,

        BLOCKING = 25,
        ATTACK_IS_BLOCKED = 27,
    }
}