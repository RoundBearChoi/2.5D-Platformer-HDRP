using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterData : MonoBehaviour
    {
        CharacterControl control;

        [Space(15)] public BlockingObjData blockingData;
        [Space(15)] public LedgeGrabData ledgeGrabData;
        [Space(15)] public RagdollData ragdollData;
        [Space(15)] public ManualInputData manualInputData;
        [Space(15)] public BoxColliderData boxColliderData;
        [Space(15)] public VerticalVelocityData verticalVelocityData;
        [Space(15)] public DamageData damageData;
        [Space(15)] public MomentumData momentumData;
        [Space(15)] public RotationData rotationData;
        [Space(15)] public JumpData jumpData;
        [Space(15)] public CollisionSphereData collisionSphereData;
        [Space(15)] public InstaKillData instaKillData;
        [Space(15)] public GroundData groundData;
        [Space(15)] public AttackData attackData;
        [Space(15)] public AnimationData animationData;

        private void Start()
        {
            control = this.gameObject.GetComponentInParent<CharacterControl>();

            blockingData = new BlockingObjData
            {
                FrontBlockingDicCount = 0,
                UpBlockingDicCount = 0,
            };

            ledgeGrabData = new LedgeGrabData
            {
                isGrabbingLedge = false,
            };

            ragdollData = new RagdollData
            {
                RagdollTriggered = false,
                flyingRagdollData = new FlyingRagdollData(),
            };

            manualInputData = new ManualInputData
            {

            };

            boxColliderData = new BoxColliderData
            {
                IsUpdatingSpheres = false,
                IsLanding = false,

                Size_Update_Speed = 0f,
                Center_Update_Speed = 0f,

                TargetSize = Vector3.zero,
                TargetCenter = Vector3.zero,
                LandingPosition = Vector3.zero,
            };

            verticalVelocityData = new VerticalVelocityData
            {
                NoJumpCancel = false,
                MaxWallSlideVelocity = Vector3.zero,
            };

            damageData = new DamageData
            {
                BlockedAttack = null,
                hp = 1f,
                damageTaken = new DamageTaken(null, null, null, null, Vector3.zero),
            };

            momentumData = new MomentumData
            {
                Momentum = 0f,
            };

            rotationData = new RotationData
            {
                LockTurn = false,
                UnlockTiming = 0f,
            };

            jumpData = new JumpData
            {
                DicJumped = new Dictionary<int, bool>(),
                CanWallJump = false,
                CheckWallBlock = false,
            };

            collisionSphereData = new CollisionSphereData
            {
                BottomSpheres = new GameObject[5],
                FrontSpheres = new GameObject[10],
                BackSpheres = new GameObject[10],
                UpSpheres = new GameObject[5],

                FrontOverlapCheckers = new OverlapChecker[10],
            };

            SubComponent[] arr = control.gameObject.GetComponentsInChildren<SubComponent>();
            foreach(SubComponent s in arr)
            {
                s.InitComponent();
            }
        }
    }
}