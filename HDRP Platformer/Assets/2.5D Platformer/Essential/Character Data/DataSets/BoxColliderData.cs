using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class BoxColliderData
    {
        public bool IsUpdatingSpheres = false;
        public bool IsLanding = false;

        public float Size_Update_Speed = 0f;
        public float Center_Update_Speed = 0f;

        public Vector3 TargetSize = new Vector3();
        public Vector3 TargetCenter = new Vector3();
        public Vector3 LandingPosition = new Vector3();
    }
}