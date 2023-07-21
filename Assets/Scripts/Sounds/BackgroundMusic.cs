using System;
using UnityEngine;

namespace Sounds
{
    public enum BackgroundMusicType
    {
        Menu,
        Game
    }

    public class BackgroundMusicData : SoundData<BackgroundMusicType>
    {
        
    }
    
    public class BackgroundMusic : SoundSingularProducer<BackgroundMusicData>
    {
        [SerializeField] private AudioClip menuClip;
        [SerializeField] private AudioClip gameClip;
        
        public override Type ProducerType => typeof(BackgroundMusic);

        protected override AudioClip GetAudioClip()
        {
            return Data.Type switch
            {
                BackgroundMusicType.Menu => menuClip,
                BackgroundMusicType.Game => gameClip,
                _ => null
            };
        }
    }
}