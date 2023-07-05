using System;
using UnityEngine;

namespace Moving
{
    [Serializable]
    public struct PlayerMovingData
    {
        [SerializeField] private float borderValue;
        [SerializeField] private float borderTolerance;
        [SerializeField] private float movingTolerance;
        [SerializeField] private float movingSpeed;

        public float BorderValue => borderValue;
        public float BorderTolerance => borderTolerance;
        public float MovingTolerance => movingTolerance;
        public float MovingSpeed => movingSpeed;

    }
}