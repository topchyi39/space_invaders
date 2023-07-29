using Buildings;
using Combat.Projectiles;
using Entities;
using Entities.EnemyEntity;
using Entities.PlayerEntity;
using GameComponents;
using UnityEngine;
using Zenject;

namespace Combat
{
    public class EnemiesCombatSystem : EntityCombatSystem, IGameListener
    {
        private EnemiesContainer _enemiesContainer;
        private IEntity _lastShootingEntity;
        private IEntity _currentShootingEntity;

        [Inject]
        private void Construct(EnemiesContainer enemiesContainer, Game game)
        {
            _enemiesContainer = enemiesContainer;
            enemiesContainer.AddEnemySpawnedCallback(enemy => enemy.SetCombatSystem(this)); 
            game.AddListener(this);
        }
        
        public void OnGameStarted()
        {
            Enable();
        }

        public void OnGamePaused()
        {
            Disable();
        }

        public void OnGameResumed()
        {
            Enable();
        }

        public void OnGameEnded()
        {
            Disable();
        }

        public void OnGameDispose()
        {
            
            Disable();
        }

        protected override void UpdateCallback()
        {
            TryFire();
        }
        
        private bool TryFire()
        {
            if (!Enabled) return false;
            if (_enemiesContainer.CurrentEnemiesCount <= 1) return false;
            if (!IsReadyToFire()) return false;
            
            Fire();
            return true;
        }
        
        protected override void ConfigureProjectile(Projectile projectile)
        {
            projectile.UpdateTargets(typeof(Player), typeof(BuildingPart));
        }

        protected override void LaunchProjectile(Projectile projectile)
        {
            _currentShootingEntity = _enemiesContainer.GetRandomAliveEntity(_lastShootingEntity);
            projectile.Launch(_currentShootingEntity.MovingSystem.Position, Origin.Down, PROJECTILE_SPEED);
        }
    }
}