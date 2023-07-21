using System;
using Combat;
using Combat.Projectiles;
using Moving;
using ObjectPolling;
using UnityEngine;
using Visual;
using VisualEffects;
using Zenject;

namespace Entities.EnemyEntity
{
    public struct EnemyParams
    {
        public EnemyType EnemyType;
    }

    public enum EnemyType
    {
        Down = 0,
        Middle = 1,
        Upper = 2
    }
    
    public class Enemy : MonoBehaviour, IEntity
    {
        [SerializeField] private EnemyVisual[] visuals;
        [SerializeField] private BoxCollider boxCollider;

        private EnemyVisual _currentVisual;
        private VisualEffectPool _visualEffectPool;
        
        public EnemyType Type { get; private set; }
        public EntityBoundary EntityBoundary => _currentVisual.Boundary;
        public IMovingSystem MovingSystem { get; private set; }
        public ICombatSystem CombatSystem { get; private set; }
        public bool IsAlive { get; private set; } = true;

        public event Action OnDied;

        [Inject]
        private void Construct(EnemyParams enemyParams, VisualEffectPool visualEffectPool)
        {
            _visualEffectPool = visualEffectPool;
            Type = enemyParams.EnemyType;
            
            _currentVisual = visuals[(int)Type];
            boxCollider.size = _currentVisual.Boundary.Bound;
        }

        private void Awake()
        {
            MovingSystem = GetComponent<IMovingSystem>();

            _currentVisual.Enable();
            (MovingSystem as EnemyMovingSystem)?.SetEnemyVisual(_currentVisual);
            MovingSystem.SetEntityBoundary(_currentVisual.Boundary);
            MovingSystem.Enable();
        }

        public void SetCombatSystem(ICombatSystem combatSystem)
        {
            CombatSystem = combatSystem;
        }
        
        public bool Hit(Projectile projectile)
        {
            Die();
            return true;
        }

        private void Die()
        {
            IsAlive = false;
            MovingSystem.Disable();
            gameObject.SetActive(false);
            _visualEffectPool.SpawnEffect<DestroyingEffect>(transform.position, transform.rotation);
            OnDied?.Invoke();
            OnDied = null;
        }

        public class Factory : PlaceholderFactory<EnemyParams, VisualEffectPool, Enemy> { }
    }
}