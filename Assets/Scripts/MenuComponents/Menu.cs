using GameComponents;
using UI;
using UI.ViewModels;
using UnityEngine;
using Zenject;

namespace MenuComponents
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private MenuParallax menuParallax;
        
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
            _menuViewModel = new MenuViewModel(this, menuParallax);
            _uiManager.BindAndShow(_menuViewModel);
        }

        public void PlayGame()
        {
            _uiManager.Hide(_menuViewModel);
            _game.StarGame();
        }
    }
}