﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "New State", menuName = "Roundbeargames/CharacterAbilities/SpawnObject")]
    public class SpawnObject : CharacterAbility
    {
        public PoolObjectType ObjectType;
        [Range(0f, 1f)]
        public float SpawnTiming;
        public string ParentObjectName = string.Empty;
        public bool StickToParent;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (SpawnTiming == 0f)
            {
                SpawnObj(characterState.control);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!characterState.control.SPAWNED_OBJ_DATA.ObjList.Contains(ObjectType))
            {
                if (stateInfo.normalizedTime >= SpawnTiming)
                {
                    SpawnObj(characterState.control);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (characterState.control.SPAWNED_OBJ_DATA.ObjList.Contains(ObjectType))
            {
                characterState.control.SPAWNED_OBJ_DATA.ObjList.Remove(ObjectType);
            }
        }

        private void SpawnObj(CharacterControl control)
        {
            if (control.SPAWNED_OBJ_DATA.ObjList.Contains(ObjectType))
            {
                return;
            }

            GameObject obj = PoolManager.Instance.GetObject(ObjectType);

            Debug.Log("spawning " + ObjectType.ToString() + " | looking for: " + ParentObjectName);

            if (!string.IsNullOrEmpty(ParentObjectName))
            {
                GameObject p = control.GetGameObject(typeof(GetChildObj), ParentObjectName);
                obj.transform.parent = p.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (!StickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);

            control.SPAWNED_OBJ_DATA.ObjList.Add(ObjectType);
        }
    }
}