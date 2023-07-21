using System;
using UnityEngine;

namespace Moving
{
    [Serializable]
    public struct PlayerMovingData
    {
        [SerializeField] private float movingTolerance;
        [SerializeField] private float movingSpeed;

        public float MovingTolerance => movingTolerance;
        public float MovingSpeed => movingSpeed;

    }
}