﻿using UnityEngine;

namespace Roundbeargames
{
    public abstract class CharacterAbility : ScriptableObject
    {
        public abstract void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
    }
}