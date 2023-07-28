using Buildings;
using Combat.Projectiles;
using Entities;
using Entities.EnemyEntity;
using Input;
using Zenject;

namespace Combat
{
    public class PlayerCombatSystem : EntityCombatSystem
    {
        private IPlayerInput _playerInput;
        private EntityBoundary _boundary;        
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

        public override void SetEntityBoundary(EntityBoundary entityBoundary)
        {
            _boundary = entityBoundary;
        }

        protected override void ConfigureProjectile(Projectile projectile)
        {
            projectile.UpdateTargets(typeof(Enemy), typeof(BuildingPart));
        }

        protected override void LaunchProjectile(Projectile projectile)
        {
            projectile.Launch(transform.position + transform.up * (_boundary.HalfExtends.y + projectile.ColliderSize.z), Origin.Up, PROJECTILE_SPEED);
        }
    }
}