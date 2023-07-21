using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ObjectPolling
{
    public abstract class AbstractObjectPools<T0> : MonoBehaviour where T0 : MonoBehaviour
    {
        [SerializeField] private T0[] prefabs;
        [SerializeField] private int defaultCapacity = 5;
        [SerializeField] private int maxPoolSize = 5;
        
        protected Dictionary<Type, T0> _prefabs;
        protected Dictionary<Type, IObjectPool<T0>> _pools;
        
        private void Awake()
        {
            InitializePrefabs();
            InitializePools();
        }

        private void InitializePrefabs()
        {
            _prefabs = new();

            foreach (var prefab in prefabs)
            {
                _prefabs.Add(prefab.GetType(), prefab);
            }
        }

        private void InitializePools()
        {
            _pools = new();
            foreach (var type in _prefabs.Keys)
            {
                var prefab = _prefabs[type];
                _pools.Add(type, new ObjectPool<T0>(
                    ()=> CreatePoolItem(prefab),
                    OnGetObject,
                    OnReleaseObject,
                    OnDestroyPoolObject,
                    true,
                    defaultCapacity,
                    maxPoolSize));
            }
        }
        private void OnReleaseObject(T0 poolObject)
        {
            poolObject.gameObject.SetActive(false);
            OnReleaseObjectCallBack(poolObject);
        }
        
        private void OnGetObject(T0 poolObject)
        {
            poolObject.gameObject.SetActive(true);
            OnGetObjectCallBack(poolObject);
        }

        private void OnDestroyPoolObject(T0 poolObject)
        {
            OnDestroyPoolObjectCallBack(poolObject);
            Destroy(poolObject.gameObject);
        }
        
        protected abstract T0 CreatePoolItem<T1>(T1 prefab) where T1 : T0;
        protected abstract void OnReleaseObjectCallBack(T0 poolObject);
        protected abstract void OnGetObjectCallBack(T0 poolObject);
        protected abstract void OnDestroyPoolObjectCallBack(T0 poolObject);
    }
}