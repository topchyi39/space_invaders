using System;

namespace Settings.Sound
{
    [Serializable]
    public class SoundSettingsModule : SettingsModule
    {
        private SoundSettingsReferences _references;

        private SoundSettings _masterSettings;
        private SoundSettings _musicSettings;

        private const string MasterPrefsKey = "_masterIsOn";        
        private const string MusicPrefsKey = "_musicIsOn";
        
        public bool MasterIsOn => _masterSettings.Value;
        public bool MusicIsOn => _musicSettings.Value;
        
        public SoundSettingsModule(SettingsManager settingsManager, SoundSettingsReferences references)
        {
            settingsManager.AddModule(this);
            _references = references;
            
            _masterSettings =
                new SoundSettings(_references.Mixer, _references.MasterVolumeParameter, MasterPrefsKey);
            _musicSettings =
                new SoundSettings(_references.Mixer, _references.MusicVolumeParameter, MusicPrefsKey);
        }

        public override void Init()
        {
           _masterSettings.Init();
           _musicSettings.Init();
        }

        public void ToggleMusic()
        {
            _musicSettings.Toggle();
        }

        public void ToggleMaster()
        {
            _masterSettings.Toggle();
        }
    }
}