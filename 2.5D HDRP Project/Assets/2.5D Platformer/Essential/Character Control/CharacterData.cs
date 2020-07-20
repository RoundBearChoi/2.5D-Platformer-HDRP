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

            SubComponent[] arr = control.gameObject.GetComponentsInChildren<SubComponent>();
            foreach(SubComponent s in arr)
            {
                s.InitComponent();
            }
        }
    }
}