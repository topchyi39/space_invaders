using ObjectPolling;
using UnityEngine;
using VisualEffects;
using Zenject;

namespace Feedbacks
{
    public class VisualEffectInstancer : MonoBehaviour
    {
        private VisualEffectPool _pool;
        
        [Inject]
        private void Construct(VisualEffectPool visualEffectPool)
        {
            _pool = visualEffectPool;
        }

        public void SpawnAndPlayEffect<T>(Vector3 position, Quaternion rotation) where T : BasePoolEffect
        {
            _pool.SpawnEffect<T>(position, rotation);
        }

        public void SpawnAndPlayEffect(string vfxType, Vector3 position, Quaternion rotation)
        {
            _pool.SpawnEffect(vfxType, position, rotation);
        }
    }
}