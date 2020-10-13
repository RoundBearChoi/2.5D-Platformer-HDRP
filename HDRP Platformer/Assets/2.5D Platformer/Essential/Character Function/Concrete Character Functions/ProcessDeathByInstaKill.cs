using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class ProcessDeathByInstaKill : CharacterFunction
    {
        public override void RunFunction(CharacterControl attacker)
        {
            control.ANIMATION_DATA.CurrentRunningAbilities.Clear();
            attacker.ANIMATION_DATA.CurrentRunningAbilities.Clear();

            control.RIGID_BODY.useGravity = false;
            control.boxCollider.enabled = false;
            control.characterSetup.
                SkinnedMeshAnimator.runtimeAnimatorController = control.INSTA_KILL_DATA.Animation_B;

            attacker.RIGID_BODY.useGravity = false;
            attacker.boxCollider.enabled = false;
            attacker.characterSetup.
                SkinnedMeshAnimator.runtimeAnimatorController = control.INSTA_KILL_DATA.Animation_A;

            Vector3 dir = control.transform.position - attacker.transform.position;

            if (dir.z < 0f)
            {
                attacker.RunFunction(typeof(FaceForward), false);
            }
            else if (dir.z > 0f)
            {
                attacker.RunFunction(typeof(FaceForward), true);
            }

            control.transform.LookAt(control.transform.position + (attacker.transform.forward * 5f), Vector3.up);
            control.transform.position = attacker.transform.position + (attacker.transform.forward * 0.45f);

            control.DAMAGE_DATA.hp = 0f;
        }
    }
}