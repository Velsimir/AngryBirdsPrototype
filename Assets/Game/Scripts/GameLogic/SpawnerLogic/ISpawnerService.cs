using UnityEngine;

namespace Game.Scripts.SpawnerLogic
{
    public interface ISpawnerService<TSpawnableObjet> where TSpawnableObjet : ISpawnable
    {
        public TSpawnableObjet Spawn(Vector3 position, Quaternion rotation);

        public TInterface SpawnAs<TInterface>(Vector3 position, Quaternion rotation)
            where TInterface : class, ISpawnable;

        public TSpawnableObjet Spawn(Transform spawnPoint);

        public TInterface SpawnAs<TInterface>(Transform spawnPoint)
            where TInterface : class, ISpawnable;
    }
}