using System;
using System.Collections.Generic;
using Combat;
using GameComponents;
using ObjectPolling;
using UnityEngine;
using Zenject;

namespace Entities.EnemyEntity
{
    public class EnemySpawner : MonoBehaviour, IGameStartListener, IGameDisposable
    {
        [SerializeField] private Transform origin;
        [Header("Prefabs")]
        [SerializeField] private Enemy upperLineEnemy;
        [SerializeField] private Enemy middleLineEnemy;
        [SerializeField] private Enemy downLineEnemy;
        [Header("Grid")]
        [SerializeField] private float positionMultiplier;
        [SerializeField] private int width;
        [SerializeField] private int height;

        private int _lineCountForOneTypeOfEnemy;
        private Game _game;
        private VisualEffectPool _visualEffectPool;
        private Enemy.Factory _factory;
        private List<Enemy> _enemies = new();
        public List<Enemy> Enemies => _enemies;
        public int Width => width;
        public int Height => height;
        
        public event Action OnEnemiesSpawned;

        [Inject]
        private void Construct(Game game, VisualEffectPool visualEffectPool, Enemy.Factory enemyFactory)
        {
            _game = game;
            _game.AddListener(this);
            _visualEffectPool = visualEffectPool;
            _factory = enemyFactory;
        }
        
        private void Start()
        {
            _lineCountForOneTypeOfEnemy = height / 3;
            
            if (height % 3 != 0)
                _lineCountForOneTypeOfEnemy++;
            
        }

        private void SpawnEnemies()
        {
            var positionOffset = GetOffset();
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var enemyType = GetEnemyTypeByHeightPosition(i);
                    var position = GetSpawnPosition(j, i, positionOffset); 
                    var enemyObject = Instantiate(enemyType, position, origin.rotation, transform);
                    Enemies.Add(enemyObject);
                }
            }
            OnEnemiesSpawned?.Invoke();
        }

        private Vector3 GetSpawnPosition(int i, int j, Vector3 offset)
        {
            var position = origin.right * i + origin.up * j - offset;
            return origin.position + position * positionMultiplier;
        }

        private Vector3 GetOffset()
        {
            return (origin.right * (width - 1) +
                    origin.up * (height - 1)) / 2f;
        }

        private Enemy Instantiate(EnemyType enemyType, Vector3 position, Quaternion rotation, Transform parent)
        {
            var enemy = _factory.Create(new EnemyParams{ EnemyType = enemyType}, _visualEffectPool);
            enemy.transform.SetParent(parent);
            enemy.transform.position = position;
            enemy.transform.rotation = rotation;

            return enemy;
        }

        private EnemyType GetEnemyTypeByHeightPosition(int heightPosition)
        {
            if (heightPosition < _lineCountForOneTypeOfEnemy)
            {
                return EnemyType.Down;
            }
            else if (heightPosition < _lineCountForOneTypeOfEnemy * 2)
            {
                return EnemyType.Middle;
            }
            else
            {
                return EnemyType.Upper;
            }
            
        }

        public void OnGameStarted()
        {
            Debug.Log("EnemySpawnerGameStarted");
            SpawnEnemies();
        }

        public void OnGameDispose()
        {
            Debug.LogError("Dispose Enemies");
            DestroyAllEnemies();
        }

        private void DestroyAllEnemies()
        {
            foreach (var enemy in Enemies)
            {
                Destroy(enemy.gameObject);
            }
            
            Enemies.Clear();
        }
    }
}