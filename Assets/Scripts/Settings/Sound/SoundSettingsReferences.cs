using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Settings.Sound
{
    [Serializable]
    public class SoundSettingsReferences
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string masterVolumeParameter;
        [SerializeField] private string musicVolumeParameter;
        
        public AudioMixer Mixer => mixer;
        public string MasterVolumeParameter => masterVolumeParameter;
        public string MusicVolumeParameter => musicVolumeParameter;
    }
}