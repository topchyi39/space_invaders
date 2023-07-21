using System;
using UnityEngine;

namespace Sounds
{
    public enum ProjectileSoundType
    {
        Launch,
        Hit
    }
    
    public class ProjectileSoundData : SoundData<ProjectileSoundType>
    {
    }
    
    public class ProjectileSound : SoundPoolProducer<ProjectileSoundData>
    {
        [SerializeField] private AudioClip launchClip;
        [SerializeField] private AudioClip hitClip;
        
        public override Type ProducerType => typeof(ProjectileSound);

        protected override AudioClip GetAudioClip()
        {
            return Data.Type switch
            {
                ProjectileSoundType.Launch => launchClip,
                ProjectileSoundType.Hit => hitClip,
                _ => throw new ArgumentOutOfRangeException()
            };
        }


    }
}