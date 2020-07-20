using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum CharacterUpdateType
    {
        MANUALINPUT,
        LEDGECHECKER,
        RAGDOLL,
        BLOCKINGOBJECTS,
        BOX_COLLIDER_UPDATER,
        VERTICAL_VELOCITY,
        DAMAGE_DETECTOR,
        COLLISION_SPHERES,
        INSTA_KILL,
        PLAYER_ATTACK,
        PLAYER_ANIMATION,
        PLAYER_ROTATION,

        COUNT,
    }
}