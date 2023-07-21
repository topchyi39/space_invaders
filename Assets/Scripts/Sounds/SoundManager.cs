using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Sounds
{
    public interface ISoundData
    {
        
    }
    
    public abstract class SoundProducer : MonoBehaviour
    {
        public abstract Type ProducerType { get; }
        public abstract void PlaySound<T>(T data) where T : ISoundData;

        [Inject]
        private void BaseConstruct(SoundManager soundManager)
        {
            soundManager.AddProducer(ProducerType, this);
        }
    }
    
    public abstract class BaseSoundProducer<TSoundData> : SoundProducer where TSoundData : class, ISoundData
    {
        [SerializeField] private AudioMixerGroup outputAudioMixerGroup;
        [SerializeField, Range(0, 1)] protected float volume;
        [SerializeField] private bool pitchIsRandom;
        [SerializeField, Range(0, 1)] private float pitchRandomRange;
        [SerializeField] private bool isFade;
        [SerializeField, Range(0, 1)] protected float fadeTime;
        [SerializeField] private bool loop;
        

        
        protected TSoundData Data;
        protected SoundEffectParams? _effectParams;
        
        
        public override void PlaySound<T>(T data) 
        {
            if (data is not TSoundData) return;
            ConfigureParams();
            Data = data as TSoundData;
            var audioSource = GetSoundEffect();
            var audioClip = GetAudioClip();
            
            audioSource.Play(audioClip, _effectParams.Value);
        }

        protected abstract SoundEffect GetSoundEffect();
        protected abstract AudioClip GetAudioClip();

        private void ConfigureParams()
        {
            if (!_effectParams.HasValue)
            {
                _effectParams = new SoundEffectParams(
                    outputAudioMixerGroup,
                    volume,
                    pitchIsRandom,
                    pitchRandomRange,
                    isFade,
                    fadeTime,
                    loop);
            }
        }
    }
    
    public class SoundManager : MonoBehaviour
    {
        private Dictionary<Type, SoundProducer> _producers = new();

        public void AddProducer(Type type, SoundProducer producer)
        {
            if (_producers.ContainsKey(type)) return;
            
            _producers.Add(type, producer);
        }

        public void PlaySound<TProducer>(ISoundData data) where TProducer : SoundProducer
        {
            if (!_producers.TryGetValue(typeof(TProducer), out var producer)) return;
            
            producer.PlaySound(data);
        }
    }
}