using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterSetup : MonoBehaviour
    {
        [Space(15)] public LedgeSetup ledgeSetup;
        [Space(15)] public Attack MarioStompAttack;
        [Space(15)] public Attack AxeThrow;
        [Space(15)] public AttackPartSetup attackPartSetup;
        [Space(15)] public Animator SkinnedMeshAnimator;
        [Space(15)] public PlayableCharacterType playableCharacterType;
    }
}