using System;
using Entities.EnemyEntity;
using UI;
using UI.ViewModels;
using UnityEngine;
using Zenject;

namespace GameComponents.Score
{
    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField] private int scorePointsPerKill;

        private int _currentScore;
        private ScoreViewModel _scoreViewModel;
        private UIManager _uiManager;
        
        [Inject]
        private void Construct(UIManager uiManager, EnemiesContainer enemiesContainer)
        {
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
    }
    
}