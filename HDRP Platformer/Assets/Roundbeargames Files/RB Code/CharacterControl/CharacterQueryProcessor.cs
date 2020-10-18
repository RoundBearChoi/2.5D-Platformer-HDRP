using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterQueryProcessor : MonoBehaviour
    {
        public Dictionary<System.Type, CharacterQuery> DicQueries = new Dictionary<System.Type, CharacterQuery>();
        public CharacterQueryList QueryListType;

        private void Start()
        {
            if (QueryListType != null)
            {
                List<System.Type> functions = QueryListType.GetList();

                foreach (System.Type t in functions)
                {
                    AddQuery(t);
                }
            }
            else
            {
                Debug.Log("Loading Default Character Queries: " + this.transform.root.gameObject.name);
                SetDefaultQueries();
            }
        }

        void SetDefaultQueries()
        {
            AddQuery(typeof(LeftSideIsBlocked));
            AddQuery(typeof(RightSideIsBlocked));
            AddQuery(typeof(FacingAttacker));
            AddQuery(typeof(ForwardReversed));
            AddQuery(typeof(FacingForward));
            AddQuery(typeof(RightFootIsForward));
            AddQuery(typeof(DoubleTapUp));
            AddQuery(typeof(DoubleTapDown));
            AddQuery(typeof(CharacterDead));

            AddQuery(typeof(FrontBlockingCharacterList));
            AddQuery(typeof(FrontBlockingObjList));
            AddQuery(typeof(FrontIsBlocked));
            AddQuery(typeof(GetAttackingPart));
            AddQuery(typeof(GetChildObj));
            AddQuery(typeof(GetTouchingMeleeWeapon));
            AddQuery(typeof(CurrentAbility));
            AddQuery(typeof(ShouldShowHitParticles));
            AddQuery(typeof(BlockedAttack));
            AddQuery(typeof(AttackIsValid));
            AddQuery(typeof(IsCollidingWithAttack));
            AddQuery(typeof(StateNameMatches));
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