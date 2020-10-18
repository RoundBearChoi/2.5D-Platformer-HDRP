using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "Character Query Type", menuName = "Roundbeargames/CharacterQueryTypes/Sumo")]
    public class SumoCharacterQueries : CharacterQueryList
    {
        public override List<Type> GetList()
        {
            QueryTypes.Clear();

            QueryTypes.Add(typeof(LeftSideIsBlocked));
            QueryTypes.Add(typeof(RightSideIsBlocked));
            QueryTypes.Add(typeof(FacingAttacker));
            QueryTypes.Add(typeof(ForwardReversed));
            QueryTypes.Add(typeof(FacingForward));
            QueryTypes.Add(typeof(RightFootIsForward));
            QueryTypes.Add(typeof(DoubleTapUp));
            QueryTypes.Add(typeof(DoubleTapDown));
            QueryTypes.Add(typeof(CharacterDead));

            QueryTypes.Add(typeof(FrontBlockingCharacterList));
            QueryTypes.Add(typeof(FrontBlockingObjList));
            QueryTypes.Add(typeof(FrontIsBlocked));
            QueryTypes.Add(typeof(GetAttackingPart));
            QueryTypes.Add(typeof(GetChildObj));
            QueryTypes.Add(typeof(GetTouchingMeleeWeapon));
            QueryTypes.Add(typeof(CurrentAbility));
            QueryTypes.Add(typeof(ShouldShowHitParticles));
            QueryTypes.Add(typeof(BlockedAttack));
            QueryTypes.Add(typeof(AttackIsValid));
            QueryTypes.Add(typeof(IsCollidingWithAttack));
            QueryTypes.Add(typeof(StateNameMatches));

            return QueryTypes;
        }
    }
}