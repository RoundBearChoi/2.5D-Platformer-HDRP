using System.Collections.Generic;

namespace Roundbeargames
{
    public class Datasets: UnityEngine.MonoBehaviour
    {
        //temp
        public MoveData MOVE_DATA;
        public DamageData DAMAGE_DATA;
        public BlockingObjData BLOCKING_DATA;
        public LedgeGrabData LEDGE_GRAB_DATA;
        public RagdollData RAGDOLL_DATA;
        public ManualInputData MANUAL_INPUT_DATA;
        public BoxColliderData BOX_COLLIDER_DATA;
        public VerticalVelocityData VERTICAL_VELOCITY_DATA;
        public TransitionData TRANSITION_DATA;
        public AbilityData ABILITY_DATA;
        public SpawnedObjData SPAWNED_OBJ_DATA;
        public RotationData ROTATION_DATA;
        public JumpData JUMP_DATA;
        public CollisionSphereData COLLISION_SPHERE_DATA;
        public GroundData GROUND_DATA;
        public AttackData ATTACK_DATA;
        public CollidingObjData COLLIDING_OBJ_DATA;
        public WeaponData WEAPON_DATA;
        public TurnData TURN_DATA;
        public CameraData CAMERA_DATA;

        private Dictionary<CharacterDataType, DatasetBase> DicDatasets = new Dictionary<CharacterDataType, DatasetBase>();

        public void InitDatasets()
        {
            DicDatasets.Clear();

            Dataset_Movement dataset_movement = new Dataset_Movement();

            DicDatasets.Add(CharacterDataType.MOVE_DATA, dataset_movement);
        }

        public float GetFloat(CharacterDataType dataType, int dataIndex)
        {
            if (DicDatasets.ContainsKey(dataType))
            {
                DatasetBase set = DicDatasets[dataType];
                
                if (set.DicFloats.ContainsKey(dataIndex))
                {
                    return set.DicFloats[dataIndex];
                }
            }

            return 0;
        }
    }
}