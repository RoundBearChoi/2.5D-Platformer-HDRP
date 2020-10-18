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
        [Space(15)] public PlayableCharacterType playableCharacterType;

        private void Awake()
        {
            CharacterControl control = this.transform.root.gameObject.GetComponent<CharacterControl>();

            SetLedgeColliders(control);
            SetDamageData(control);
        }

        void SetLedgeColliders(CharacterControl control)
        {
            LedgeCollider[] col_arr = this.transform.root.gameObject.GetComponentsInChildren<LedgeCollider>();

            foreach (LedgeCollider c in col_arr)
            {
                if (c.gameObject.name.Contains("1"))
                {
                    control.LEDGE_GRAB_DATA.collider1 = c;
                }

                if (c.gameObject.name.Contains("2"))
                {
                    control.LEDGE_GRAB_DATA.collider2 = c;
                }
            }
        }

        void SetDamageData(CharacterControl control)
        {
            control.DAMAGE_DATA.MarioStompAttack = MarioStompAttack;
            control.DAMAGE_DATA.AxeThrow = AxeThrow;
        }
    }
}