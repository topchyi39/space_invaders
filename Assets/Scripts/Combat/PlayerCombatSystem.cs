using Combat.Projectiles;
using Entities.EnemyEntity;
using Input;
using Zenject;

namespace Combat
{
    public class PlayerCombatSystem : EntityCombatSystem
    {
        private IPlayerInput _playerInput;
        
        [Inject]
        private void Construct(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        protected override void UpdateCallback()
        {
            TryFire();            
        }

        private void TryFire()
        {
            if(_playerInput.Fire && IsReadyToFire() && Enabled)
                Fire();
        }

        protected override void ConfigureProjectile(Projectile projectile)
        {
            projectile.UpdateTargets(typeof(Enemy));
        }

        protected override void LaunchProjectile(Projectile projectile)
        {
            projectile.Launch(transform.position, Origin.Up, PROJECTILE_SPEED);
        }
    }
}