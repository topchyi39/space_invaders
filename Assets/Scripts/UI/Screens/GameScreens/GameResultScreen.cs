using System;
using Ads;
using GameComponents;
using GameComponents.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WalletComponents;

namespace UI.Screens.GameScreens
{
    public class GameResultViewModel : IViewModel
    {
        protected Game _game;
        protected ScoreHandler _scoreHandler;
        private MobileAds _mobileAds;
        private IWalletCollectable _walletCollectable;
        private bool _wasMultiply;
        public GameResult GameResult { get; }
        public int ScorePoints => _scoreHandler.CurrentScore;
        public bool Best => _scoreHandler.IsBest;

        public GameResultViewModel(Game game, ScoreHandler scoreHandler, MobileAds mobileAds,
            IWalletCollectable walletCollectable, GameResult gameResult)
        {
            _game = game;
            _scoreHandler = scoreHandler;
            GameResult = gameResult;
            _mobileAds = mobileAds;
            _walletCollectable = walletCollectable;
        }

        public void RetryGame()
        {
            if (!_wasMultiply)
            {
                _mobileAds.ShowInterstitialAd(_game.RestartGame);
            }
            else
                _game.RestartGame();

            _wasMultiply = false;
        }
        
        public event Action OnDataChanged;

        public void MultiplyGold()
        {
            _wasMultiply = true;
            _mobileAds.ShowRewardInterstitialAd(reward =>
            {
                if (reward is { Amount: > 0 })
                {
                    _walletCollectable.MultiplyGoldDuringGame(2);
                }
            });
        }
    }
    
    public class GameResultScreen : Screen<GameResultViewModel>
    {
        [SerializeField] private Button retryButton;
        [SerializeField] private Button goldMultiply;
        [SerializeField] private TMP_Text scorePointsText;
        [SerializeField] private BestScoreMark bestScoreMark;
        
        private void Start()
        {
            retryButton.onClick.AddListener(RetryGame);
            goldMultiply.onClick.AddListener(MultiplyGold);
        }

        private void RetryGame()
        {
            ViewModel.RetryGame();
        }

        private void MultiplyGold()
        {
            ViewModel.MultiplyGold();
            goldMultiply.gameObject.SetActive(false);
        }

        protected override void ShowCallback()
        {
            scorePointsText.text = ViewModel.ScorePoints.ToString();
            bestScoreMark.SetMark(ViewModel.Best);
            
            goldMultiply.gameObject.SetActive(ViewModel.GameResult == GameResult.Win);
        }
    }
}