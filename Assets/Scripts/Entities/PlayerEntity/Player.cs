using System;
using Combat;
using Combat.Projectiles;
using GameComponents;
using MoreMountains.Feedbacks;
using Moving;
using UnityEngine;
using Visual.PlayerVisual;
using Visual.PlayerVisualComponents;
using Zenject;

namespace Entities.PlayerEntity
{
    public class Player : MonoBehaviour, IEntity, IGamePauseListener, IGameResumeListener
    {
        public class Factory : PlaceholderFactory<Game, PlayerSelector, Player> { }

        [SerializeField] private MMF_Player destroyFeedbacks;
        [SerializeField] private PlayerVisual visual;
        
        private Game _game;
        
        public EntityBoundary EntityBoundary { get; private set; }
        public IMovingSystem MovingSystem { get; private set; }
        public ICombatSystem CombatSystem { get; private set; }
        public bool IsAlive { get; private set; } = true;
        public event Action OnDied;

        [Inject]
        private void Construct(Game game, PlayerSelector selector)
        {
            _game = game;
            _game.AddListener(this);
            visual.ChangeModel(selector.CurrentModel);
        }

        private void Awake()
        {
            MovingSystem = GetComponent<IMovingSystem>();
            CombatSystem = GetComponent<ICombatSystem>();
            EntityBoundary = GetComponent<EntityBoundary>();
            
            MovingSystem.SetEntityBoundary(EntityBoundary);
            CombatSystem.SetEntityBoundary(EntityBoundary);
            EnableComponents();
        }
        
        public bool Hit(Projectile projectile)
        {
            Die();
            return true;
        }

        [ContextMenu("Die")]
        private void Die()
        {
            MovingSystem.Disable();
            CombatSystem.Disable();
            destroyFeedbacks.PlayFeedbacks();
            IsAlive = false;
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
