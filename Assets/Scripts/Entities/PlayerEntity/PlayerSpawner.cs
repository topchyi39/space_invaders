using System;
using GameComponents;
using UnityEngine;
using Zenject;

namespace Entities.PlayerEntity
{
    public class PlayerSpawner : MonoBehaviour, IGameStartListener, IGameDisposable
    {
        [SerializeField] private Transform spawnPoint;

        private Game _game;
        private Player.Factory _playerFactory;
        private Player _player;
        
        [Inject]
        private void Construct(Game game, Player.Factory playerFactory)
        {
            _game = game;
            _game.AddListener(this);
            _playerFactory = playerFactory;
        }

        public void OnGameStarted()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            _player = _playerFactory.Create(_game);
            var playerTransform = _player.transform;
            
            playerTransform.parent = spawnPoint.parent;
            playerTransform.position = spawnPoint.position;
            playerTransform.rotation = spawnPoint.rotation;
        }

        public void OnGameDispose()
        {
            Destroy(_player.gameObject);
            _player = null;
        }
    }
}