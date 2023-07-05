using System;
using UnityEngine;

namespace Moving
{
    [Serializable]
    public struct PlayerRotationData
    {
        [SerializeField] private float angleRotation;
        [SerializeField] private float rotationSpeed;

        public float AngleRotation => angleRotation;
        public float RotationSpeed => rotationSpeed;
    }
}