using System;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace UI.Views
{
    public class SoundControlGroup : MonoBehaviour
    {
        [SerializeField] private ToggleButton masterSoundToggleButton;
        [SerializeField] private ToggleButton musicSoundToggleButton;
        
        private UIManager _uiManager;
        private SettingsScreen _settingsScreen;

        [Inject]
        private void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        } 
        
        private void Awake()
        {
            _settingsScreen = _uiManager.GetScreen<SettingsScreen>();
            _settingsScreen.AddMusicToggleButton(this);
        }

        public void SetValueMaster(bool value, bool withAction)
        {
            SetValue(masterSoundToggleButton, value, withAction);
            musicSoundToggleButton.gameObject.SetActive(value);
        }

        public void SetValueMusic(bool value, bool withAction) => SetValue(musicSoundToggleButton, value, withAction);

        private void SetValue(ToggleButton button, bool value, bool withAction)
        {
            button.SetValue(value, withAction);
        }

        public void AddListeners(Action masterSoundAction, Action musicSoundAction)
        {
            masterSoundAction += MasterSoundToggled;
            masterSoundToggleButton.AddListener(masterSoundAction);
            musicSoundToggleButton.AddListener(musicSoundAction);
        }

        private void MasterSoundToggled()
        {
            musicSoundToggleButton.gameObject.SetActive(masterSoundToggleButton.Value);
        }
    }
}