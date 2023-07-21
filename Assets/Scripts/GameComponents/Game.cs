using System;
using System.Collections.Generic;
using UI;
using UI.Screens.GameScreens;
using UI.ViewModels;
using UnityEngine;
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
        private UIManager _uiManager;
        private GameViewModel _gameViewModel;
        private GameResultViewModel _lastResult; 
        
        public event Action OnGameStarted;
        public event Action OnGamePaused;
        public event Action OnGameResumed;
        public event Action OnGameEnded;
        public event Action OnGameDispose;

        [Inject]
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
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

        public void AddListener(object listener)
        {
            if (listener is IGameListener gameListener)
            {
                AddListener(gameListener);
                return;
            }
            
            if (listener is IGameStartListener startListener)
                OnGameStarted += startListener.OnGameStarted;
            if (listener is IGamePauseListener pauseListener)
                OnGamePaused += pauseListener.OnGamePaused;
            if (listener is IGameResumeListener resumeListener)
                OnGameResumed += resumeListener.OnGameResumed;
            if (listener is IGameEndListener endListener)
                OnGameEnded += endListener.OnGameEnded;
            if (listener is IGameDisposable disposable)
                OnGameDispose += disposable.OnGameDispose;
        }

        public void StarGame()
        {
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
            _lastResult = new GameResultViewModel(this, result);
            _uiManager.BindAndShow(_lastResult);
            OnGameEnded?.Invoke();
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