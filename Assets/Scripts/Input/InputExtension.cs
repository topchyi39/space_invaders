using UnityEngine;

namespace Input
{
    public static class InputExtension
    {
        public static Quaternion ToUnityRotation(this Quaternion gyroRotation)
        {
            return new Quaternion(gyroRotation.x, gyroRotation.y, -gyroRotation.z, -gyroRotation.w);
        }
    }
}