using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Moving
{
    public struct EnemiesMoveJob : IJob
    {
        public Vector3 direction;
        public float speed;
        public float deltaTime;
        public bool useDeltaTime;
        public NativeArray<Vector3> startEnemiesPositions;
        public NativeArray<Vector3> resultPositions;

        public void Execute()
        {
            var velocity = direction * speed * (useDeltaTime ? deltaTime : 1f);
            for (var i = 0; i < startEnemiesPositions.Length; i++)
            {
                resultPositions[i] = startEnemiesPositions[i] + velocity;
            }

        }
    }
}