using System.Collections.Generic;

namespace Roundbeargames
{
    public enum CharacterDataType
    {
        NONE,

        MOVE_DATA,
        DAMAGE_DATA,
        BLOCKING_DATA,
        LEDGE_GRAB_DATA,
        RAGDOLL_DATA,
        MANUAL_INPUT_DATA,
        BOX_COLLIDER_DATA,
        VERTICAL_VELOCITY_DATA,
        TRANSITION_DATA,
        ABILITY_DATA,
        SPAWNED_OBJ_DATA,
        ROTATION_DATA,
        JUMP_DATA,
        COLLISION_SPHERE_DATA,
        GROUND_DATA,
        ATTACK_DATA,
        COLLIDING_OBJ_DATA,
        WEAPON_DATA,
        TURN_DATA,
        CAMERA_DATA,
    }

    public abstract class DatasetBase
    {
        public Dictionary<int, float> DicFloats = new Dictionary<int, float>();

        public void ClearDics()
        {
            DicFloats.Clear();
        }

        public abstract void DefineDataset();
    }
}