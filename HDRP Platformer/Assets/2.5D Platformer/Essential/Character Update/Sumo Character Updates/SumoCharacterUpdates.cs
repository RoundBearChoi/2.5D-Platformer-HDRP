using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "Character Update Type", menuName = "Roundbeargames/CharacterUpdateTypes/Sumo")]
    public class SumoCharacterUpdates : CharacterUpdateList
    {
        public override List<Type> GetList()
        {
            UpdateTypes.Clear();

            UpdateTypes.Add(typeof(LedgeChecker));
            UpdateTypes.Add(typeof(DamageDetector));
            UpdateTypes.Add(typeof(Ragdoll));
            UpdateTypes.Add(typeof(BlockingObj));
            UpdateTypes.Add(typeof(BoxColliderUpdater));
            UpdateTypes.Add(typeof(VerticalVelocity));
            UpdateTypes.Add(typeof(PlayerRotation));
            UpdateTypes.Add(typeof(PlayerJump));
            UpdateTypes.Add(typeof(PlayerGround));
            UpdateTypes.Add(typeof(PlayerAttack));
            UpdateTypes.Add(typeof(PlayerAnimation));
            UpdateTypes.Add(typeof(CollisionSpheres));

            return UpdateTypes;
        }
    }
}