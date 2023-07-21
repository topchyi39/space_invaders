using System;
using System.Collections.Generic;
using CameraTools;
using Cysharp.Threading.Tasks;
using GameComponents;
using ObjectPolling;
using UnityEngine;
using Zenject;

namespace Combat.Projectiles
{
    public class ProjectileMoving : MonoBehaviour, IGamePauseListener, IGameResumeListener
    {
        private bool _gameIsPaused;
        private bool _calculatePositions;
        private CameraBoundary _cameraBoundary;
        private ProjectilePool _projectilePool;
        
        private readonly List<Projectile> _availableProjectiles = new();
        private readonly List<Projectile> _tempProjectiles = new();

        [Inject]
        private void Construct(CameraBoundary cameraBoundary, Game game, ProjectilePool projectilePool)
        {
            _cameraBoundary = cameraBoundary;
            _projectilePool = projectilePool;
            _projectilePool.OnGetPoolObject += AddProjectile;
            _projectilePool.OnRealesePoolObject += RemoveProjectile;
            game.AddListener(this);
        }
        
        private void FixedUpdate()
        {
            if (_gameIsPaused) return;
            _calculatePositions = true;
            var deltaTime = Time.deltaTime;

            var count = _availableProjectiles.Count;
            for (var i = 0; i < count; i++)
            {
                var projectile = _availableProjectiles[i];
                if (projectile.CanMove)
                {
                    var nextPosition = projectile.transform.position + projectile.Direction * (projectile.Speed * deltaTime);
                    
                    if (!_cameraBoundary.CheckPointInVerticalBoundary(nextPosition, projectile.CameraBoundary.HalfExtends.z)) projectile.ReleaseProjectile(true);
                    else projectile.transform.position = nextPosition;
                }
            }
            _calculatePositions = false;
        }

        public async void AddProjectile(Projectile projectile)
        {
            await UniTask.WaitWhile(() => _calculatePositions);
            _availableProjectiles.Add(projectile);
        }

        public async void RemoveProjectile(Projectile projectile)
        {
            await UniTask.WaitWhile(() => _calculatePositions);
            _availableProjectiles.Remove(projectile);
        }

        public void OnGamePaused()
        {
            _gameIsPaused = true;
        }

        public void OnGameResumed()
        {
            _gameIsPaused = false;
        }
    }
}