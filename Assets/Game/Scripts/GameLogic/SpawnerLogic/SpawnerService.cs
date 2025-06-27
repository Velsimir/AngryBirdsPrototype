using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.GameLogic.SpawnerLogic
{
    public class SpawnerService<T> : ISpawnerService<T>
        where T : ISpawnable<T>
    {
        private readonly ObjectPoolService _pool;

        public SpawnerService(T prefab, Transform container = null)
        {
            _pool = new ObjectPoolService(prefab, container);
        }

        public T Spawn(Vector3 position, Quaternion rotation)
        {
            T instance = _pool.Get();
            instance.MonoBehaviour.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        public TInterface SpawnAs<TInterface>(Vector3 position, Quaternion rotation)
            where TInterface : class, ISpawnable<T>
        {
            var instance = Spawn(position, rotation);
            return instance as TInterface;
        }

        public T Spawn(Transform spawnPoint)
        {
            return Spawn(spawnPoint.position, spawnPoint.rotation);
        }

        public TInterface SpawnAs<TInterface>(Transform spawnPoint)
            where TInterface : class, ISpawnable<T>
        {
            return SpawnAs<TInterface>(spawnPoint.position, spawnPoint.rotation);
        }

        private class ObjectPoolService
        {
            private readonly T _prefab;
            private readonly Transform _container;
            private readonly List<T> _available = new();

            public ObjectPoolService(T prefab, Transform container = null)
            {
                _prefab = prefab;
                _container = container;
            }

            public T Get()
            {
                T instance;

                if (_available.Count > 0)
                {
                    instance = _available[0];
                    _available.RemoveAt(0);
                }
                else
                {
                    instance = InstantiateFromPrefab();
                    instance.Disappeared += ReturnToPool;
                }

                instance.MonoBehaviour.gameObject.SetActive(true);
                return instance;
            }

            private void ReturnToPool(T instance)
            {
                instance.MonoBehaviour.gameObject.SetActive(false);
                _available.Add(instance);
            }

            private T InstantiateFromPrefab()
            {
                var obj = Object.Instantiate(_prefab.MonoBehaviour, _container);
                return obj.GetComponent<T>();
            }
        }
    }
}