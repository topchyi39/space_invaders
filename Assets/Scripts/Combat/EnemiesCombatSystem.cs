using Combat.Projectiles;
using Entities;
using Entities.EnemyEntity;
using Entities.PlayerEntity;
using GameComponents;
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
            
        }

        protected override void UpdateCallback()
        {
            TryFire();
        }
        
        private bool TryFire()
        {
            if (!IsReadyToFire() || !Enabled) return false;
            
            //Fire();
            return true;
        }
        
        protected override void ConfigureProjectile(Projectile projectile)
        {
            projectile.UpdateTargets(typeof(Player));
        }

        protected override void LaunchProjectile(Projectile projectile)
        {
            _currentShootingEntity = _enemiesContainer.GetRandomEntity(_lastShootingEntity);
            projectile.Launch(_currentShootingEntity.MovingSystem.Position, Origin.Down, PROJECTILE_SPEED);
        }
    }
}