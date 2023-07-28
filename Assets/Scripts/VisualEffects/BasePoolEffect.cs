
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

        private Color _defaultColor;

        private void Awake()
        {
            _defaultColor = effect.GetVector4("Color");
        }

        private IEnumerator EffectLife()
        {
            yield return new WaitUntil(() => effect.aliveParticleCount > 0);
            yield return new WaitWhile(() => effect.aliveParticleCount > 0);
            EffectEnded?.Invoke();
        }

        public void Play(Color color)
        {
            effect.SetVector4("Color", color);
            effect.Play();
            StartCoroutine(EffectLife());
        }

        public void Play()
        {
            effect.SetVector4("Color", _defaultColor);
            effect.Play();
            StartCoroutine(EffectLife());
        }

    }
}