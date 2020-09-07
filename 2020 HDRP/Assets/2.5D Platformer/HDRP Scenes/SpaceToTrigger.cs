using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class SpaceToTrigger : MonoBehaviour
    {
        private void Start()
        {
            foreach (Transform t in this.transform)
            {
                Debug.Log(t.gameObject.name);

                t.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach(Transform t in this.transform)
                {
                    Debug.Log(t.gameObject.name);

                    t.gameObject.SetActive(false);
                    t.gameObject.SetActive(true);
                }
            }
        }
    }
}