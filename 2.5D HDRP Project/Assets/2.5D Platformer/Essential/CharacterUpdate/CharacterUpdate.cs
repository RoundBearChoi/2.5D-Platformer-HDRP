using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum CharacterUpdateType
    {
        MANUALINPUT,
        LEDGECHECKER,
        RAGDOLL,
        BLOCKINGOBJECTS,
        BOX_COLLIDER_UPDATER,
        VERTICAL_VELOCITY,
        DAMAGE_DETECTOR,
        COLLISION_SPHERES,
        INSTA_KILL,
        PLAYER_ATTACK,
        PLAYER_ANIMATION,
        PLAYER_ROTATION,

        COUNT,
    }

    public abstract class CharacterUpdate : MonoBehaviour
    {
        protected CharacterUpdateProcessor characterUpdateProcessor;

        public CharacterControl control
        {
            get
            {
                return characterUpdateProcessor.control;
            }
        }

        private void Awake()
        {
            characterUpdateProcessor = this.gameObject.GetComponentInParent<CharacterUpdateProcessor>();
        }

        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();

        public virtual void InitComponent()
        {

        }
    }
}