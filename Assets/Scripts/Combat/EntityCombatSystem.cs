using System;
using Combat.Projectiles;
using Entities;
using GameComponents;
using ObjectPolling;
using OriginContainer;
using UnityEngine;
using Zenject;

namespace Combat
{
    public abstract class EntityCombatSystem : MonoBehaviour, ICombatSystem
    {
        [SerializeField] private CombatData combatData;
        
        private ProjectilePool _projectilePool;
        private bool _enabled;
        private bool _isReadyToFire;
        private float _currentCooldownTime;
        
        protected IOrigin Origin;
        protected const float PROJECTILE_SPEED = 10f;

        public bool Enabled => _enabled;

        [Inject]
        private void BaseConstruct(ProjectilePool projectilePool, IOrigin origin, Game game)
        {
            game.AddListener(this);
            _projectilePool = projectilePool;
            Origin = origin;
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        public virtual void SetEntityBoundary(EntityBoundary entityBoundary) { }

        private void Update()
        {
            if (!_enabled) return;
            
            UpdateCooldown();
            UpdateCallback();
        }

        protected virtual void UpdateCallback() { }
        
        private void UpdateCooldown()
        {
            if (_isReadyToFire) return;
            
            if (_currentCooldownTime < combatData.FireRateInSeconds) 
                _currentCooldownTime += Time.deltaTime;
            else 
                _isReadyToFire = true;
        }

        private void ResetCooldown()
        {
            _currentCooldownTime = 0f;
            _isReadyToFire = false;
        }
        
        protected void Fire()
        {
            ResetCooldown();
            var projectile = GetProjectile();
            LaunchProjectile(projectile);
        }

        private Projectile GetProjectile()
        {
            var projectile = _projectilePool.Pool.Get();
            ConfigureProjectile(projectile);
            return projectile;
        }

        protected bool IsReadyToFire()
        {
            return _isReadyToFire;
        }
        
        protected abstract void ConfigureProjectile(Projectile projectile);
        protected abstract void LaunchProjectile(Projectile projectile);
    }
}