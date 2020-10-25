using UnityEngine;
using System.Collections.Generic;

namespace Roundbeargames
{
    public enum CharacterDataType
    {
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

        COUNT,
    }

    [System.Serializable]
    public class DatasetBase
    {
        [SerializeField] protected string name;
        [SerializeField] protected FloatData[] arrFloats;

        public void SetName(CharacterDataType dataType)
        {
            name = dataType.ToString();
        }

        public virtual void SetDefaultValues()
        {
            throw new System.NotImplementedException();
        }

        public virtual void InitDataset()
        {
            throw new System.NotImplementedException();
        }

        public virtual float GetFloat(int dataIndex)
        {
            throw new System.NotImplementedException();
        }

        public virtual void SetFloat(int dataIndex, float value)
        {
            throw new System.NotImplementedException();
        }
    }
}