using System;
using System.Collections.Generic;
using System.Linq;
using GameComponents;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.EnemyEntity
{
    public class EnemiesContainer : MonoBehaviour, IGameDisposable
    {
        [SerializeField] private EnemySpawner enemySpawner;

        private Game _game;
            
        private List<Enemy> _enemies = new ();
        private int _diedEnemies;
        private Action<Enemy> _enemyConfigureAction;
        
        public bool IsEnemiesSpawned { get; private set; }
        public int DefaultEnemiesCount => _enemies.Count;
        public int CurrentEnemiesCount => _enemies.Count(enemy => enemy.IsAlive);
        public int Width { get; private set; }
        public int Height { get; private set; }

        [Inject]
        private void Construct(Game game)
        {
            _game = game;
            _game.AddListener(this);
        }
        
        private void Awake()
        {
            Width = enemySpawner.Width;
            Height = enemySpawner.Height;
            enemySpawner.OnEnemiesSpawned += EnemiesSpawned;
        }

        private void EnemiesSpawned()
        {
            IsEnemiesSpawned = true;
            _enemies = enemySpawner.Enemies;
            
            foreach (var enemy in _enemies)
            {
                _enemyConfigureAction?.Invoke(enemy);
                enemy.OnDied += OnEnemyDied;
            }
        }

        public void OnGameDispose()
        {
            IsEnemiesSpawned = false;
            // _enemies.Clear();
        }

        public Enemy[] GetEnemiesByLine(int lineIndex)
        {
            return lineIndex >= Height ? 
                                null   : 
                                _enemies.GetRange(lineIndex * Width, Width).Where(enemy => enemy.IsAlive ).ToArray();
        }

        public void AddEnemySpawnedCallback(Action<Enemy> action)
        {
            _enemyConfigureAction += action;
        }

        public IEntity GetRandomAliveEntity(IEntity previousEntity)
        {
            var aliveEnemies = _enemies.Where(entity => entity.IsAlive).ToArray();
            var randomIndex = Random.Range(0, aliveEnemies.Length);
            IEntity randomEntity;
            
            if (previousEntity == null)
            {
                randomEntity =  aliveEnemies[randomIndex];
            }
            
            else
            {
                do
                {
                    randomEntity = aliveEnemies[randomIndex];
                } 
                while (randomEntity == previousEntity);
            }

            return randomEntity;
        }

        [ContextMenu("Kill All")]
        private void KillAll()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Hit(null);
            }
        }

        
        [ContextMenu("Left One Enemy")]
        private void LeftOneEnemy()
        {
            var randomEnemy = GetRandomAliveEntity(null);
            foreach (var enemy in _enemies)
            {
                if (enemy != randomEnemy)
                    enemy.Hit(null);
            }
        }
        
        private void OnEnemyDied()
        {
            _diedEnemies++;

            if (_diedEnemies == _enemies.Count)
            {
                _game.EndGame(GameResult.Win);
            }
        }
    }
}