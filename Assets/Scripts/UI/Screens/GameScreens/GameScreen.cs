using UI.Screens.SubScreens;
using UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class GameScreen : Screen<GameViewModel>
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button toMenuButton;
        
        [SerializeField] private SubScreen inGameSubScreen;
        [SerializeField] private SubScreen pausedSubScreen;
        [SerializeField] private SubScreen gameResultSubScreen;

        private SubScreen _currentSubScreen;
        
        private void Awake()
        {
            pauseButton.onClick.AddListener(PauseGame);
            resumeButton.onClick.AddListener(ResumeGame);
            toMenuButton.onClick.AddListener(ToMenu);
        }

        private void PauseGame()
        {
            ViewModel?.PauseGame();
            SwapSubScreen(pausedSubScreen);
        }

        private void ResumeGame()
        {
            ViewModel?.ResumeGame();
            SwapSubScreen(inGameSubScreen);
        }

        private void ToMenu()
        {
            ViewModel?.DisposeGame();
            _uiManager.Show<MenuViewModel>();
        }

        private void SwapSubScreen(SubScreen subScreen)
        {
            if(_currentSubScreen) _currentSubScreen.Hide();

            _currentSubScreen = subScreen;
            _currentSubScreen.Show();
        }

        protected override void ShowCallback()
        {
            SwapSubScreen(inGameSubScreen);
        }

        protected override void BindCallback(GameViewModel viewModel)
        {
            viewModel.OnGameEnded += OnGameEnded;
        }

        private void OnGameEnded()
        {
            SwapSubScreen(gameResultSubScreen);
        }
    }
}