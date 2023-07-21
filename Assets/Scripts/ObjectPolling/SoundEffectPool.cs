using Sounds;
using UnityEngine;

namespace ObjectPolling
{
    public class SoundEffectPool : AbstractObjectPool<SoundEffect>
    {
        
        
        protected override SoundEffect CreatePoolItem()
        {
            var soundEffect = Instantiate(Prefab, transform);
            soundEffect.Release += () => Pool.Release(soundEffect);
            return soundEffect;
        }

        protected override void OnReleaseObjectCallBack(SoundEffect poolObject) { }
        protected override void OnGetObjectCallBack(SoundEffect poolObject) { }
        protected override void OnDestroyPoolObjectCallBack(SoundEffect poolObject) { }
    }
}