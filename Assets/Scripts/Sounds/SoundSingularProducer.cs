namespace Sounds
{
    public abstract class SoundSingularProducer<TSoundData> : SoundPoolProducer<TSoundData> where TSoundData : class, ISoundData
    {
        private SoundEffect _soundEffect;

        protected override SoundEffect GetSoundEffect()
        {
            if (_soundEffect && _soundEffect.IsPlaying)
            {
                _soundEffect.Stop(true, fadeTime);
                _soundEffect = _soundEffectPool.Pool.Get();
                return _soundEffect;
            }
            
            if(_soundEffect == null  || !_soundEffect.gameObject.activeSelf) _soundEffect = _soundEffectPool.Pool.Get();
            return _soundEffect;
        }
    }
}