using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerAttack : CharacterUpdate
    {
        public override void InitComponent()
        {

        }

        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {

            if (control.Attack)
            {
                if (control.DATASET.ATTACK_DATA.AttackButtonIsReset)
                {
                    control.DATASET.ATTACK_DATA.AttackTriggered = true;
                    control.DATASET.ATTACK_DATA.AttackButtonIsReset = false;
                }
            }
            else
            {
                control.DATASET.ATTACK_DATA.AttackButtonIsReset = true;
                control.DATASET.ATTACK_DATA.AttackTriggered = false;
            }
        }

        public override void OnLateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}