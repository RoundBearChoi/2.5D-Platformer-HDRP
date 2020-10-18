using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterData : MonoBehaviour
    {
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