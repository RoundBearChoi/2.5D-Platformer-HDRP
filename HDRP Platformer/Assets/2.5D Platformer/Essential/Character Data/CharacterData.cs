using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterData : MonoBehaviour
    {
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
        [Space(15)] public GroundData groundData;
        [Space(15)] public AttackData attackData;
        [Space(15)] public AnimationData animationData;
        [Space(15)] public CollidingObjData collidingObjData;
        [Space(15)] public WeaponData weaponData;
        [Space(15)] public CameraData cameraData;
        [Space(15)] public TurnData turnData;


        private void Start()
        {
            CharacterControl control = this.transform.root.gameObject.GetComponent<CharacterControl>();

            LedgeCollider[] col_arr = this.transform.root.gameObject.GetComponentsInChildren<LedgeCollider>();
            LedgeCollider c1 = null;
            LedgeCollider c2 = null;

            foreach(LedgeCollider c in col_arr)
            {
                if (c.gameObject.name.Contains("1"))
                {
                    c1 = c;
                }

                if (c.gameObject.name.Contains("2"))
                {
                    c2 = c;
                }
            }

            ledgeGrabData = new LedgeGrabData
            {
                isGrabbingLedge = false,
                collider1 = c1,
                collider2 = c2,
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

                MarioStompAttack = control.characterSetup.MarioStompAttack,
                AxeThrow = control.characterSetup.AxeThrow,
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
            };

            groundData = new GroundData
            {

            };

            attackData = new AttackData
            {
                AttackButtonIsReset = false,
                AttackTriggered = false,
            };

            animationData = new AnimationData
            {
                InstantTransitionMade = false,
                LatestMoveForward = null,
                LatestMoveUp = null,
                LockTransition = false,
                IsIgnoreCharacterTime = false,
                CurrentRunningAbilities = new Dictionary<CharacterAbility, int>(),
            };

            collidingObjData = new CollidingObjData
            {
                CollidingBodyParts = new Dictionary<TriggerDetector, List<Collider>>(),
                CollidingWeapons = new Dictionary<TriggerDetector, List<Collider>>(),
            };

            weaponData = new WeaponData
            {
                HoldingWeapon = null,
            };

            cameraData = new CameraData
            {
                CameraShaken = false,
            };

            turnData = new TurnData
            {
                StartedForward = true,
            };

            control.characterUpdateProcessor.InitUpdaters();
        }
    }
}