using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterData : MonoBehaviour
    {
        [Space(15)] public GroundData groundData;
        [Space(15)] public AttackData attackData;
        [Space(15)] public CollidingObjData collidingObjData;
        [Space(15)] public WeaponData weaponData;
        [Space(15)] public CameraData cameraData;
        [Space(15)] public TurnData turnData;

        private void Start()
        {
            CharacterControl control = this.transform.root.gameObject.GetComponent<CharacterControl>();

            groundData = new GroundData
            {

            };

            attackData = new AttackData
            {
                AttackButtonIsReset = false,
                AttackTriggered = false,
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