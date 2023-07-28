using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Sounds
{
    public struct SoundEffectParams
    {
        public AudioMixerGroup outputAudioMixerGroup;
        public float volume;
        public bool randomPitch;
        public float pitchRange;
        public bool isFade;
        public float fadeTime;
        public bool loop;

        public SoundEffectParams(AudioMixerGroup outputAudioMixerGroup, float volume, bool randomPitch, float pitchRange, bool isFade, float fadeTime, bool loop)
        {
            this.outputAudioMixerGroup = outputAudioMixerGroup;
            this.volume = volume;
            this.randomPitch = randomPitch;
            this.pitchRange = pitchRange;
            this.isFade = isFade;
            this.fadeTime = fadeTime;
            this.loop = loop;
        }
    }
    
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        public bool IsPlaying => audioSource.isPlaying;

        public event Action Release;

        public void Play(AudioClip clip, SoundEffectParams effectParams)
        {
            SetParametersToAudioSource(effectParams);
            PlayClip(clip, effectParams);
            StartCoroutine(WaitForAudioSourcePlayed());
        }

        public void Stop(bool fade = false, float fadeTime = 0.25f)
        {
            if (fade)
                audioSource.DOFade(0, fadeTime).onComplete += () => audioSource.Stop();
            else 
                audioSource.Stop();

        }

        private void PlayClip(AudioClip clip, SoundEffectParams effectParams)
        {
            audioSource.clip = clip;
            
            if (effectParams.isFade)
            {
                audioSource.volume = 0;
                audioSource.Play();
                audioSource.DOFade(effectParams.volume, effectParams.fadeTime);
            }
            else if (effectParams.loop)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }
        }


        private void SetParametersToAudioSource(SoundEffectParams effectParams)
        {
            audioSource.volume = effectParams.volume;
            audioSource.outputAudioMixerGroup = effectParams.outputAudioMixerGroup;
            audioSource.pitch = !effectParams.randomPitch ? 1f : GetRandomPitch(effectParams.pitchRange);
            audioSource.loop = effectParams.loop;
        }

        private IEnumerator WaitForAudioSourcePlayed()
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
            Release?.Invoke();
        }

        private float GetRandomPitch(float range)
        {
            return Random.Range(1f - range, 1f + range);
        }
    }
}