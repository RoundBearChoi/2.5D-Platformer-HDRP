using UnityEngine;

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

        [Space(10)]

        [SerializeField]
        DatasetBase[] arrSets;

        public void InitDatasets()
        {
            arrSets = new DatasetBase[(int)CharacterDataType.COUNT];

            arrSets[(int)CharacterDataType.MOVE_DATA] = new Dataset_Movement();

            for (int i = 0; i < arrSets.Length; i++)
            {
                if (arrSets[i] != null)
                {
                    arrSets[i].SetName((CharacterDataType)i);
                    arrSets[i].InitDataset();
                }
            }

            //testing
            SetFloat(CharacterDataType.MOVE_DATA, (int)MovementData_Floats.MOMENTUM, 10f);
        }

        public float GetFloat(CharacterDataType dataType, int dataIndex)
        {
            DatasetBase set = arrSets[(int)dataType];

            return set.GetFloat(dataIndex);
        }

        public void SetFloat(CharacterDataType dataType, int dataIndex, float value)
        {
            DatasetBase set = arrSets[(int)dataType];

            set.SetFloat(dataIndex, value);
        }
    }
}