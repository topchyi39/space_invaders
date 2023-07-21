using System;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPolling
{
    public abstract class AbstractObjectPool<T0> : MonoBehaviour where T0 : MonoBehaviour
    {
        [SerializeField] private T0 prefab;
        [SerializeField] private int defaultCapacity = 5;
        [SerializeField] private int maxPoolSize = 5;
        
        private IObjectPool<T0> _pool;

        public T0 Prefab => prefab;

        public event Action<T0> OnGetPoolObject;
        public event Action<T0> OnRealesePoolObject;
        
        public IObjectPool<T0> Pool
        {
            get
            {
                _pool ??= new ObjectPool<T0>(CreatePoolItem,
                                            OnGetObject,
                                            OnReleaseObject,
                                            OnDestroyPoolObject,
                                            true,
                                            defaultCapacity,
                                            maxPoolSize);
                
                return _pool;
            }
        }


        private void OnReleaseObject(T0 poolObject)
        {
            poolObject.gameObject.SetActive(false);
            OnRealesePoolObject?.Invoke(poolObject);
            OnReleaseObjectCallBack(poolObject);
        }
        
        private void OnGetObject(T0 poolObject)
        {
            poolObject.gameObject.SetActive(true);
            OnGetPoolObject?.Invoke(poolObject);
            OnGetObjectCallBack(poolObject);
        }

        private void OnDestroyPoolObject(T0 poolObject)
        {
            OnDestroyPoolObjectCallBack(poolObject);
            Destroy(poolObject.gameObject);
        }

        protected abstract T0 CreatePoolItem();
        protected abstract void OnReleaseObjectCallBack(T0 poolObject);
        protected abstract void OnGetObjectCallBack(T0 poolObject);
        protected abstract void OnDestroyPoolObjectCallBack(T0 poolObject);
    }
}