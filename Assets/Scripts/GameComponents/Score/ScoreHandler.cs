using System;
using Entities.EnemyEntity;
using UI;
using UI.ViewModels;
using UnityEngine;
using Zenject;

namespace GameComponents.Score
{
    public class ScoreHandler : MonoBehaviour, IGameDisposable, IGameEndListener
    {
        [SerializeField] private int scorePointsPerKill;

        private int _currentScore;
        private ScoreViewModel _scoreViewModel;
        private UIManager _uiManager;
        private BestScore _bestScore;

        public int CurrentScore => _currentScore;
        public bool IsBest => _bestScore.CheckForBest(_currentScore);
        
        [Inject]
        private void Construct(UIManager uiManager, EnemiesContainer enemiesContainer, Game game)
        {
            game.AddListener(this);
            _uiManager = uiManager;
            enemiesContainer.AddEnemySpawnedCallback(SubscribeOnEnemyDied);
            _scoreViewModel = new ScoreViewModel();
        }

        private void Start()
        {
            _uiManager.Bind(_scoreViewModel);
        }

        private void SubscribeOnEnemyDied(Enemy enemy)
        {
            enemy.OnDied += () => EnemyDied(enemy);
        }
        
        private void EnemyDied(Enemy enemy)
        {
            _currentScore += scorePointsPerKill * ((int)enemy.Type + 1);
            _scoreViewModel.UpdateScorePoints(_currentScore);
        }

        public void OnGameDispose()
        {
            _currentScore = 0;
            _scoreViewModel.UpdateScorePoints(_currentScore);
        }

        public void OnGameEnded()
        {
        }
    }
    
}