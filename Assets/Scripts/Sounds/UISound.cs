using System;
using UnityEngine;

namespace Sounds
{
    public enum UISoundType
    {
        ButtonClick
    }
    
    public class UISoundData : SoundData<UISoundType>
    {
    }

    public class UISound : SoundPoolProducer<UISoundData>
    {
        [SerializeField] private AudioClip buttonClick;
        public override Type ProducerType => typeof(UISound);

        protected override AudioClip GetAudioClip()
        {
            return Data.Type switch
            {
                UISoundType.ButtonClick => buttonClick,
                _ => null
            };
        }

    }
}