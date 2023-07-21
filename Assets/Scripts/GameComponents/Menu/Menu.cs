using System;
using UI;
using UI.ViewModels;
using UnityEngine;
using Zenject;

namespace GameComponents.Menu
{
    public class Menu : MonoBehaviour
    {
        private Game _game;
        private UIManager _uiManager;

        private MenuViewModel _menuViewModel;
        
        [Inject]
        private void Construct(Game game, UIManager uiManager)
        {
            _game = game;
            _uiManager = uiManager;
        }

        private void Start()
        {
            _menuViewModel = new MenuViewModel(this);
            _uiManager.BindAndShow(_menuViewModel);
        }

        public void PlayGame()
        {
            _uiManager.Hide(_menuViewModel);
            _game.StarGame();
        }
    }
}