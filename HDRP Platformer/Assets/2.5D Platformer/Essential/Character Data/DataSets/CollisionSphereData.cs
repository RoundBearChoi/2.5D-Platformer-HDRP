using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class CollisionSphereData
    {
        public GameObject[] BottomSpheres = new GameObject[5];
        public GameObject[] FrontSpheres = new GameObject[10];
        public GameObject[] BackSpheres = new GameObject[10];
        public GameObject[] UpSpheres = new GameObject[5];
    }
}