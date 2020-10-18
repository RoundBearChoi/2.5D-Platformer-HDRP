using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class DamageData
    {
        [Header("Damage Dataset")]
        public DamageTaken damageTaken = new DamageTaken(null, null, null, null, Vector3.zero);

        [Header("Defense")]
        public AttackCondition BlockedAttack = null;
        public float hp = 1f;

        [Header("Offense")]
        public Attack MarioStompAttack;
        public Attack AxeThrow;
    }

    [System.Serializable]
    public class DamageTaken
    {
        public DamageTaken(CharacterControl attacker,
            Attack attack,
            TriggerDetector damagee,
            GameObject damager,
            Vector3 incomingVelocity)
        {
            mAttacker = attacker;
            mAttack = attack;
            mDamagee = damagee;
            mDamager = damager;
            mIncomingVelocity = incomingVelocity;
        }

        [SerializeField] CharacterControl mAttacker = null;
        [SerializeField] Attack mAttack = null;
        [SerializeField] GameObject mDamager = null;
        [SerializeField] TriggerDetector mDamagee = null;
        [SerializeField] Vector3 mIncomingVelocity = Vector3.zero;

        public CharacterControl ATTACKER { get { return mAttacker; } }
        public Attack ATTACK { get { return mAttack; } }
        public GameObject DAMAGER { get { return mDamager; } }
        public TriggerDetector DAMAGEE { get { return mDamagee; } }
        public Vector3 INCOMING_VELOCITY { get { return mIncomingVelocity; } }
    }
}