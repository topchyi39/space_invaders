using System;
using GameComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens.GameScreens
{
    public class GameResultViewModel : IViewModel
    {
        
        protected Game _game;
        public GameResult GameResult { get; }
        
        public GameResultViewModel(Game game, GameResult gameResult)
        {
            _game = game;
            GameResult = gameResult;
        }

        public void RetryGame()
        {
            _game.RestartGame();
        }
        
        public int ScorePoints { get; protected set; }
        public event Action OnDataChanged;
    }
    
    public class GameResultScreen : Screen<GameResultViewModel>
    {
        [SerializeField] private Button retryButton;
        [SerializeField] private TMP_Text scorePointsText;

        private void Start()
        {
            retryButton.onClick.AddListener(RetryGame);
        }

        private void RetryGame()
        {
            ViewModel.RetryGame();
        }

        protected override void ShowCallback()
        {
            // scorePointsText.text = ViewModel.ScorePoints.ToString();
        }
    }
}