using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "Character Function Type", menuName = "Roundbeargames/CharacterFunctionTypes/Sumo")]
    public class SumoCharacterFunctions : CharacterFunctionList
    {
        public override List<System.Type> GetList()
        {
            FunctionTypes.Clear();

            FunctionTypes.Add(typeof(LedgeCollidersOff));
            FunctionTypes.Add(typeof(ClearRagdollVelocity));
            FunctionTypes.Add(typeof(ClearUpVelocity));
            FunctionTypes.Add(typeof(CheckDownBlocking));
            FunctionTypes.Add(typeof(CheckUpBlocking));
            FunctionTypes.Add(typeof(CheckMarioStomp));
            FunctionTypes.Add(typeof(Reposition_Front_Spheres));
            FunctionTypes.Add(typeof(Reposition_Back_Spheres));
            FunctionTypes.Add(typeof(Reposition_Bottom_Spheres));
            FunctionTypes.Add(typeof(Reposition_Up_Spheres));

            FunctionTypes.Add(typeof(SpawnHitParticles));
            FunctionTypes.Add(typeof(AddForceToDamagedPart));
            FunctionTypes.Add(typeof(MoveTransformForward));
            FunctionTypes.Add(typeof(CalculateMomentum));
            FunctionTypes.Add(typeof(FaceForward));
            FunctionTypes.Add(typeof(DoLedgeGrab));
            FunctionTypes.Add(typeof(TurnIntoFlyingRagdoll));
            FunctionTypes.Add(typeof(GetPushedAsRagdoll));
            FunctionTypes.Add(typeof(TakeDamage));
            FunctionTypes.Add(typeof(DamageReaction));
            FunctionTypes.Add(typeof(TakeDamageFromThrownWeapon));
            FunctionTypes.Add(typeof(ProcessMeleeWeaponContact));
            FunctionTypes.Add(typeof(ProcessMeleeWeaponExit));
            FunctionTypes.Add(typeof(InitCharacter));

            //AddFunction(typeof(ProcessDeathByInstaKill));

            return FunctionTypes;
        }
    }
}