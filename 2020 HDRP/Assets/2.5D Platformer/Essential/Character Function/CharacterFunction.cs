using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public abstract class CharacterFunction : MonoBehaviour
    {
        public CharacterControl control;

        public virtual void RunFunction()
        {
            throw new System.NotImplementedException();
        }

        public virtual void RunFunction(float float1)
        {
            throw new System.NotImplementedException();
        }

        public virtual void RunFunction(float float1, float float2)
        {
            throw new System.NotImplementedException();
        }

        public virtual void RunFunction(bool bool1)
        {
            throw new System.NotImplementedException();
        }

        public virtual void RunFunction(CharacterControl attacker, PoolObjectType EffectsType)
        {
            throw new System.NotImplementedException();
        }

        public virtual void RunFunction(RagdollPushType ragdollPushType)
        {
            throw new System.NotImplementedException();
        }

        public virtual void RunFunction(AttackCondition info)
        {
            throw new System.NotImplementedException();
        }
    }
}