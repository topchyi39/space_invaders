using System;
using System.Collections.Generic;
using Ads;
using Analytics;
using GameComponents.Score;
using UI;
using UI.Screens.GameScreens;
using UI.ViewModels;
using UnityEngine;
using WalletComponents;
using Zenject;

namespace GameComponents
{

    public enum GameResult
    {
        Win, 
        Lose
    }
    
    public class Game : MonoBehaviour
    {
        [SerializeField] private ScoreHandler scoreHandler;

        private UIManager _uiManager;
        private IWalletCollectable _wallet;
        private MobileAds _ads;
        private GameAnalytics _gameAnalytics;
        
        private GameViewModel _gameViewModel;
        private GameResultViewModel _lastResult;

        private DateTime  _startTime;
        
        public event Action OnGameStarted;
        public event Action OnGamePaused;
        public event Action OnGameResumed;
        public event Action OnGameEnded;
        public event Action OnGameDispose;

        [Inject]
        private void Construct(UIManager uiManager, IWalletCollectable wallet, MobileAds ads, GameAnalytics gameAnalytics)
        {
            _uiManager = uiManager;
            _wallet = wallet;
            _ads = ads;
            _gameAnalytics = gameAnalytics;
        }

        private void Start()
        {
            _gameViewModel = new GameViewModel(this);
        }
        
        public void AddListener(IGameListener listener)
        {
            OnGameStarted += listener.OnGameStarted;
            OnGamePaused += listener.OnGamePaused;
            OnGameResumed += listener.OnGameResumed;
            OnGameEnded += listener.OnGameEnded;
            OnGameDispose += listener.OnGameDispose;
        }

        public void AddListener(object listener, bool debug = false)
        {
            if (listener is IGameListener gameListener)
            {
                AddListener(gameListener);
                return;
            }

            if (listener is IGameStartListener startListener)
            {
                if (debug)
                    Debug.LogError("IGameStartListener - " + listener.GetType().Name);
                OnGameStarted += startListener.OnGameStarted;
            }

            if (listener is IGamePauseListener pauseListener)
            {
                if (debug)
                    Debug.LogError("IGamePauseListener - " + listener.GetType().Name);
                OnGamePaused += pauseListener.OnGamePaused;
            }

            if (listener is IGameResumeListener resumeListener)
            {
                if (debug)
                    Debug.LogError("IGameResumeListener - " + listener.GetType().Name);
                OnGameResumed += resumeListener.OnGameResumed;
            }

            if (listener is IGameEndListener endListener)
            {
                if (debug)
                    Debug.LogError("IGameEndListener - " + listener.GetType().Name);
                OnGameEnded += endListener.OnGameEnded;
            }

            if (listener is IGameDisposable disposable)
            {
                if (debug)
                    Debug.LogError("IGameDisposable - " + listener.GetType().Name);
                OnGameDispose += disposable.OnGameDispose;
            }
        }

        public void StarGame()
        {
            _startTime = DateTime.Now;
            _uiManager.BindAndShow(_gameViewModel);
            OnGameStarted?.Invoke();
        }

        public void PauseGame()
        {
            OnGamePaused?.Invoke();
        }

        public void ResumeGame()
        {
            OnGameResumed?.Invoke();
        }

        public void EndGame(GameResult result)
        {
            _lastResult = new GameResultViewModel(this, scoreHandler, _ads, _wallet, result);
            _uiManager.BindAndShow(_lastResult);
            var elapsed = DateTime.Now - _startTime;
            _gameAnalytics.LogLevelTime($"{elapsed.Minutes}:{elapsed.Seconds}");
            OnGameEnded?.Invoke();
        }

        public void DisposeGame()
        {
            _uiManager.Hide(_gameViewModel);
            OnGameDispose?.Invoke();
        }

        public void RestartGame()
        {
            _uiManager.Hide(_lastResult);
            _uiManager.Hide(_gameViewModel);
            OnGameDispose?.Invoke();
            
            StarGame();
        }
    }
}