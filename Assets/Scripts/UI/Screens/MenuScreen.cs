using Sounds;
using UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class MenuScreen : Screen<MenuViewModel>
    {
        [SerializeField] private Button playButton;

        private SoundManager _soundManager;
        
        [Inject]
        private void Construct(SoundManager soundManager)
        {
            _soundManager = soundManager;
        }
        
        private void Start()
        {
            playButton.onClick.AddListener(Play);
        }

        private void Play()
        {
            ViewModel.Play();
        }

        protected override void ShowCallback()
        {
            _soundManager.PlaySound<BackgroundMusic>(new BackgroundMusicData { Type = BackgroundMusicType.Menu });
        }

        protected override void HideCallback()
        {
            
            _soundManager?.PlaySound<BackgroundMusic>(new BackgroundMusicData { Type = BackgroundMusicType.Game });
        }
    }
}