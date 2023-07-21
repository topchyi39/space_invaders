using System;
using UnityEngine;

namespace OriginContainer
{
    public class Origin : MonoBehaviour, IOrigin
    {
        public Vector3 Up { get; private set; }
        public Vector3 Down { get; private set; }
        public Vector3 Left { get; private set; }
        public Vector3 Right { get; private set; }

        public Transform Transform => transform;

        private void Awake()
        {
            Up = transform.up;
            Down = -Up;
            Right = transform.right;
            Left = -Right;
        }
    }
}