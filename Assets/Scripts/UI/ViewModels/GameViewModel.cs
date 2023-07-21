using System;
using GameComponents;
using UI.Screens;

namespace UI.ViewModels
{
    public class GameViewModel : IViewModel, IGameEndListener
    {
        private readonly Game _game;
        
        public event Action OnGameEnded;
        public event Action OnDataChanged;

        public GameViewModel(Game game)
        {
            _game = game;
            _game.AddListener(this);
        }
        
        public void PauseGame()
        {
            _game.PauseGame();
        }

        public void ResumeGame()
        {
            _game.ResumeGame();
        }

        void IGameEndListener.OnGameEnded()
        {
            OnGameEnded?.Invoke();
        }
    }
}