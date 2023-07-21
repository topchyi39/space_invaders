using UnityEngine;
using VisualEffects;

namespace ObjectPolling
{
    public class VisualEffectPool : AbstractObjectPools<BasePoolEffect>
    {
        public void SpawnEffect<T1>(Vector3 position, Quaternion rotation) where T1 : BasePoolEffect
        {
            var type = typeof(T1);
            if (!_pools.ContainsKey(type)) return;
            
            var effect = _pools[type].Get() as T1;
            effect.transform.position = position;
            effect.transform.rotation = rotation;
            effect.gameObject.SetActive(true);
            effect.Play();
        }
        
        protected override BasePoolEffect CreatePoolItem<T1>(T1 prefab)
        {
            var effect = Instantiate(prefab, transform);
            effect.gameObject.SetActive(false);
            effect.EffectEnded += () => _pools[effect.GetType()].Release(effect);

            return effect;
        }

        protected override void OnReleaseObjectCallBack(BasePoolEffect poolObject)
        {
        }

        protected override void OnGetObjectCallBack(BasePoolEffect poolObject)
        {
        }

        protected override void OnDestroyPoolObjectCallBack(BasePoolEffect poolObject)
        {
        }

        public void SpawnEffect(string vfxType, Vector3 position, Quaternion rotation)
        {
            var baseType = typeof(BasePoolEffect);
            var type = baseType.Assembly.GetType(vfxType);
            if (!_pools.ContainsKey(type)) return;
            
            var effect = _pools[type].Get();
            effect.transform.position = position;
            effect.transform.rotation = rotation;
            effect.gameObject.SetActive(true);
            effect.Play();
        }
    }
}