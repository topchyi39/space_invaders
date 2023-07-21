
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

namespace VisualEffects
{
    public class BasePoolEffect : MonoBehaviour
    {        
        [SerializeField] private VisualEffect effect;
        
        public event Action EffectEnded;

        private IEnumerator EffectLife()
        {
            yield return new WaitUntil(() => effect.aliveParticleCount > 0);
            yield return new WaitWhile(() => effect.aliveParticleCount > 0);
            EffectEnded?.Invoke();
        }

        public void Play()
        {
            effect.Play();
            StartCoroutine(EffectLife());
        }

    }
}