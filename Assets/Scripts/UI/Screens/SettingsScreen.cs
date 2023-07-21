using System.Collections.Generic;
using UI.ViewModels;
using UI.Views;
using UnityEngine;

namespace UI.Screens
{
    public class SettingsScreen : Screen<SettingsViewModel>
    {
        private List<SoundControlGroup> _soundControlGroups = new();
        
        public void AddMusicToggleButton(SoundControlGroup musicButton)
        {
            _soundControlGroups.Add(musicButton);
            musicButton.AddListeners(ToggleMasterSound, ToggleMusicSound);
        }

        private void ToggleMasterSound()
        {
            ViewModel.ToggleMaster();
            UpdateMasterView();
        }
        
        public void ToggleMusicSound()
        {
            ViewModel.ToggleMusic();
            UpdateMusicView();
        }

        protected override void BindCallback(SettingsViewModel viewModel)
        {
            base.BindCallback(viewModel);

            UpdateMasterView();
            UpdateMusicView();
        }

        private void UpdateMasterView()
        {
            foreach (var soundControlGroup in _soundControlGroups)
            {
                soundControlGroup.SetValueMaster(ViewModel.MasterIsOn, false);
            }
        }

        private void UpdateMusicView()
        {
            foreach (var soundControlGroup in _soundControlGroups)
                soundControlGroup.SetValueMusic(ViewModel.MusicIsOn, false);
        }
    }
}