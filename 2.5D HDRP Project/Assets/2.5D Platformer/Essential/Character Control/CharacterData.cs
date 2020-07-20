using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterData : MonoBehaviour
    {
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
    }
}