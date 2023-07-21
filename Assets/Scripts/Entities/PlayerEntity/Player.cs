using System;
using Combat;
using Combat.Projectiles;
using GameComponents;
using MoreMountains.Feedbacks;
using Moving;
using UnityEngine;
using Zenject;

namespace Entities.PlayerEntity
{
    public class Player : MonoBehaviour, IEntity, IGamePauseListener, IGameResumeListener
    {
        public class Factory : PlaceholderFactory<Game, Player> { }

        [SerializeField] private MMF_Player destroyFeedbacks;

        private Game _game;
        
        public EntityBoundary EntityBoundary { get; private set; }
        public IMovingSystem MovingSystem { get; private set; }
        public ICombatSystem CombatSystem { get; private set; }
        public event Action OnDied;

        [Inject]
        private void Construct(Game game)
        {
            _game = game;
            _game.AddListener(this);
        }

        private void Awake()
        {
            MovingSystem = GetComponent<IMovingSystem>();
            CombatSystem = GetComponent<ICombatSystem>();
            EntityBoundary = GetComponent<EntityBoundary>();
            
            MovingSystem.SetEntityBoundary(EntityBoundary);
            EnableComponents();
        }
        
        public bool Hit(Projectile projectile)
        {
            Die();
            return true;
        }

        private void Die()
        {
            MovingSystem.Disable();
            CombatSystem.Disable();
            destroyFeedbacks.PlayFeedbacks();
            OnDied?.Invoke();
            OnDied = null;
            _game.EndGame(GameResult.Lose); 
        }
        
        public void OnGamePaused()
        {
            DisableComponents();
        }

        public void OnGameResumed()
        {
            EnableComponents();
        }

        private void EnableComponents()
        {
            MovingSystem.Enable();
            CombatSystem.Enable();
        }

        private void DisableComponents()
        {
            MovingSystem.Disable();
            CombatSystem.Disable();
        }
    }
}
