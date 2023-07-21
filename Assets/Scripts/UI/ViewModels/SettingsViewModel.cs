using System;
using Settings;
using Settings.Sound;
using UI.Screens;
using UnityEngine;

namespace UI.ViewModels
{
    public class SettingsViewModel : IViewModel
    {
        private SettingsManager _settingsManager;

        public bool MasterIsOn => _settingsManager.GetModule<SoundSettingsModule>().MasterIsOn;
        public bool MusicIsOn => _settingsManager.GetModule<SoundSettingsModule>().MusicIsOn;
        
        public event Action OnDataChanged;

        public SettingsViewModel(SettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        
        public void ToggleMusic()
        {
            _settingsManager.GetModule<SoundSettingsModule>().ToggleMusic();
        }

        public void ToggleMaster()
        {
            _settingsManager.GetModule<SoundSettingsModule>().ToggleMaster();

        }
    }
}