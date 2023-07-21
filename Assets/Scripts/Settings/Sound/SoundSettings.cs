using UnityEngine;
using UnityEngine.Audio;

namespace Settings.Sound
{
    public struct SoundSettings
    {
        private readonly AudioMixer _mixer;
        private readonly string _prefsKey;
        private readonly string _nameParameter;
        private readonly float _defaultVolume;
        
        public bool Value { get; private set; }

        private const float MinVolumeValue = -80f;
        
        public SoundSettings(AudioMixer mixer, string nameParameter, string prefsKey)
        {
            _mixer = mixer;
            _nameParameter = nameParameter;
            _prefsKey = prefsKey;
            _mixer.GetFloat(_nameParameter, out _defaultVolume);
            
            Value = PlayerPrefs.GetInt(_prefsKey, 1) == 1;
        }

        public void Init()
        {
            _mixer.SetFloat(_nameParameter, Value ? _defaultVolume : MinVolumeValue);
        }
        
        public bool Toggle()
        {
            Value = !Value;
            SetVolume();

            return Value;
        }

        private void SetVolume()
        {
            _mixer.SetFloat(_nameParameter, Value ? _defaultVolume : MinVolumeValue);
            PlayerPrefs.SetInt(_prefsKey, Value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}