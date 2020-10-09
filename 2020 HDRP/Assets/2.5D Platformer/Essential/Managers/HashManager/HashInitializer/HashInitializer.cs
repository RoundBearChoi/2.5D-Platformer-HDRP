using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class HashInitializer : MonoBehaviour
    {
        public List<AnimatorStateHashes> HashPrefabs = new List<AnimatorStateHashes>();

        private void Start()
        {
            foreach(AnimatorStateHashes prefab in HashPrefabs)
            {
                AnimatorStateHashes hashes = Instantiate(prefab);
                hashes.gameObject.transform.position = Vector3.zero;
                hashes.gameObject.transform.rotation = Quaternion.identity;
                hashes.gameObject.transform.parent = this.transform;

                foreach(HashData data in hashes.HashTypes)
                {
                    data.ShortNameHash = Animator.StringToHash(data.StateName);
                    data.classKey.ShortNameHash = Animator.StringToHash(data.StateName);
                    HashManager.Instance.DicHashes.Add(data.classKey, data.ShortNameHash);
                }
            }
        }

        public void Testing()
        {

        }
    }
}