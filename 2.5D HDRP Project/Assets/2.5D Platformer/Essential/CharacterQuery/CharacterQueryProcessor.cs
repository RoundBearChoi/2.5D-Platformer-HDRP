﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterQueryProcessor : MonoBehaviour
    {
        public Dictionary<System.Type, CharacterQuery> DicQueries = new Dictionary<System.Type, CharacterQuery>();

        private void Start()
        {
            AddQuery(typeof(LeftSideIsBlocked));
            AddQuery(typeof(RightSideIsBlocked));

            AddQuery(typeof(FrontBlockingCharacterList));
            AddQuery(typeof(FrontBlockingObjList));
            AddQuery(typeof(GetAttackingPart));
        }

        void AddQuery(System.Type type)
        {
            if (type.IsSubclassOf(typeof(CharacterQuery)))
            {
                GameObject newQ = new GameObject();
                newQ.transform.parent = this.transform;
                newQ.transform.localPosition = Vector3.zero;
                newQ.transform.localRotation = Quaternion.identity;

                CharacterQuery q = newQ.AddComponent(type) as CharacterQuery;
                DicQueries.Add(type, q);

                q.control = this.transform.root.gameObject.GetComponent<CharacterControl>();

                newQ.name = type.ToString();
                newQ.name = newQ.name.Replace("Roundbeargames.", "");
            }
        }
    }
}