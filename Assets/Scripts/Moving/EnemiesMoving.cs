﻿using System;
using System.Collections;
using System.Linq;
using CameraTools;
using Entities.EnemyEntity;
using GameComponents;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Moving
{
    public enum EnemyMoveDirection
    {
        // Down = 0, 
        Left = 0, 
        Right = 1
    }
    
    public class EnemiesMoving : MonoBehaviour, IGameListener
    {
        [SerializeField] private Transform origin;
        [SerializeField] private float maximumDelayBetweenMoving = 1f;
        [SerializeField] private float minimumDelayBetweenMoving = 0.02f;
        [SerializeField] private float defaultSpeed = 0.2f;
        [SerializeField] private float speedWhenOneEnemyLeft;
        
        private CameraBoundary _cameraBoundary;
        private EnemiesContainer _enemiesContainer;
        private Enemy[] _aliveEnemies;
        
        private int _lineIndex;

        private float _currentDelay;
        private bool _isPaused;
        private bool _jobCreated;
        private bool _reachedToBorder;
        private bool _moveHorizontal = true;// = true;
        private int _moveVerticalCount;
        private int _currentDirection;
        
        private JobHandle _jobHandle;
        private EnemiesMoveJob _job;

        [Inject]
        private void Construct(EnemiesContainer enemiesContainer, CameraBoundary cameraBoundary, Game game)
        {
            _enemiesContainer = enemiesContainer;
            _cameraBoundary = cameraBoundary;
            game.AddListener(this);
        }

        private void OnDestroy()
        {
            if (_jobCreated)
            {
                _jobHandle.Complete();
                DisposeJob();
            }
        }

        public void OnGameStarted()
        {
            StartCoroutine(MovingRoutine());
        }

        public void OnGamePaused()
        {
            _isPaused = true;
        }

        public void OnGameResumed()
        {
            _isPaused = false;
        }

        public void OnGameEnded()
        {
            StopAllCoroutines();
        }

        public void OnGameDispose()
        {
            StopAllCoroutines();
        }

        private IEnumerator MovingRoutine()
        {
            yield return new WaitUntil(() => _enemiesContainer.IsEnemiesSpawned);
            while (this)
            {
                if (!_moveHorizontal)
                {
                    _moveVerticalCount++;

                    if (_moveVerticalCount >= _enemiesContainer.Height + 1)
                    {
                        _moveVerticalCount = 0;
                        _moveHorizontal = true;
                        ChangeDirection();
                    }
                }
                
                MoveByLine(GetDirection(_moveHorizontal));
                CompleteJob();
                yield return new WaitForSeconds(GetDelay());
                yield return new WaitWhile(() => _isPaused);
                
                _lineIndex++;

                if (_lineIndex >= _enemiesContainer.Height)
                {
                    _lineIndex = 0;
                }
            }
        }

        private void MoveByLine(Vector3 direction)
        {
            UpdateEnemiesArrayByLine(_lineIndex);
            
            CreateAndRunJob(direction);
        }

        private void CreateAndRunJob(Vector3 direction)
        {
            var currentPositions = GetEnemiesPositions();
            var speed = _enemiesContainer.CurrentEnemiesCount > 1 ? defaultSpeed : speedWhenOneEnemyLeft;
            _job = new EnemiesMoveJob
            {
                direction = direction,
                speed = speed,
                deltaTime = Time.deltaTime,
                useDeltaTime = false,
                startEnemiesPositions = currentPositions,
                resultPositions = new NativeArray<Vector3>(currentPositions.Length, Allocator.TempJob)
            };

            _jobHandle = _job.Schedule();
            _jobCreated = true;
        }

        private void CompleteJob()
        {
            if (!_jobCreated) return;

            _jobHandle.Complete();

            if (IsEnemiesReachToBorder())
            {
                _moveHorizontal = false;
                _lineIndex = -1;
                DisposeJob();
                return;
            }
            
            for (var i = 0; i < _job.resultPositions.Length; i++)
            {
                _aliveEnemies[i].MovingSystem.MoveManually(_job.resultPositions[i]);
            }

            DisposeJob();
        }

        private bool IsEnemiesReachToBorder()
        {
            foreach (var resultPosition in _job.resultPositions)
            {
                var reachedToBorder = !_cameraBoundary.CheckPointInBoundary(resultPosition, 1);
                if (reachedToBorder)
                {
                    return true;
                }
            }

            return false;
        }

        private void ChangeDirection()
        {
            _currentDirection++;
            var enumLength = Enum.GetNames(typeof(EnemyMoveDirection)).Length;

            if (_currentDirection >= enumLength)
                _currentDirection = 0;
        }

        private Vector3 GetDirection(bool moveHorizontal)
        {
            if(moveHorizontal)
            {
                switch ((EnemyMoveDirection)_currentDirection)
                {
                    case EnemyMoveDirection.Left:
                        return -origin.right;
                    case EnemyMoveDirection.Right:
                        return origin.right;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                return -origin.up;
            }
        }

        private void DisposeJob()
        {
            _job.startEnemiesPositions.Dispose();
            _job.resultPositions.Dispose();
            _jobCreated = false;
        }

        private float GetDelay()
        {
            var maxEnemiesCount = _enemiesContainer.DefaultEnemiesCount;
            var currentEnemiesCount = _enemiesContainer.CurrentEnemiesCount;
            var inverseLerpValue = Mathf.InverseLerp(1, maxEnemiesCount,currentEnemiesCount);
            return Mathf.Lerp(minimumDelayBetweenMoving, maximumDelayBetweenMoving, inverseLerpValue);
        }

        private NativeArray<Vector3> GetEnemiesPositions()
        {
            var positionArray = _aliveEnemies.Select(enemy => enemy.transform.position).ToArray();
            var nativeArray = new NativeArray<Vector3>(positionArray.Length, Allocator.TempJob);
            nativeArray.CopyFrom(positionArray);
            return nativeArray;
        }

        private void UpdateEnemiesArrayByLine(int index)
        {
            _aliveEnemies = _enemiesContainer.GetEnemiesByLine(index);
            Debug.LogError($"Line index: {index}; Alive in line - {_aliveEnemies.Length}");
        }
    }
}