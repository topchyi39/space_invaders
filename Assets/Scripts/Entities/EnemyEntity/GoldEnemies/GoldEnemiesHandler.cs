using System.Collections;
using Cysharp.Threading.Tasks;
using GameComponents;
using UnityEngine;
using WalletComponents;
using Zenject;

namespace Entities.EnemyEntity.GoldEnemies
{
    public class GoldEnemiesHandler : MonoBehaviour, IGameListener
    {
        [SerializeField] private int goldReward;
        [SerializeField] private float minTimeCooldown;
        [SerializeField] private float maxTimeCooldown;
        
        private EnemiesContainer _enemiesContainer;
        private IWalletCollectable _wallet;
        private Enemy _lastEnemy;

        private bool _paused;
        
        [Inject]
        private void Construct(EnemiesContainer enemiesContainer, IWalletCollectable wallet, Game game)
        {
            _enemiesContainer = enemiesContainer;
            _wallet = wallet;
            game.AddListener(this);
        }

        public void OnGameStarted()
        {
            StartCoroutine(GoldSelectorRoutine());
        }

        public void OnGamePaused()
        {
            _paused = true;
        }

        public void OnGameResumed()
        {
            _paused = false;
        }

        public void OnGameEnded()
        {
            StopAllCoroutines();
        }

        public void OnGameDispose()
        {
            StopAllCoroutines();
        }

        private IEnumerator GoldSelectorRoutine()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(GetCooldown());
                yield return new WaitWhile(() => _paused);
                SetGoldEnemy();
                yield return new WaitWhile(() => _lastEnemy.IsAlive);
                _lastEnemy.SetGold(false);
                _wallet.Add(goldReward);
            }
        }

        private float GetCooldown()
        {
            return Random.Range(minTimeCooldown, maxTimeCooldown);
        }

        private void SetGoldEnemy()
        {
            _lastEnemy = _enemiesContainer.GetRandomAliveEntity(null) as Enemy;
            _lastEnemy.SetGold(true);
        }
    }
}