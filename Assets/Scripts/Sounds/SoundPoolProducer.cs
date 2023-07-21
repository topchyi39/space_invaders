using ObjectPolling;
using Zenject;

namespace Sounds
{
    public abstract class SoundPoolProducer<TSoundData> : BaseSoundProducer<TSoundData> where TSoundData : class, ISoundData
    {
        protected SoundEffectPool _soundEffectPool;

        [Inject]
        private void Construct(SoundEffectPool soundEffectPool)
        {
            _soundEffectPool = soundEffectPool;
        }

        protected override SoundEffect GetSoundEffect()
        {
            return _soundEffectPool.Pool.Get();
        }
    }
}