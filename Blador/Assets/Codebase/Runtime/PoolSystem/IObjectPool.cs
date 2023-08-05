using UnityEngine;

namespace Codebase.Runtime.PoolSystem
{
    public interface IObjectPool
    {
        T GetOrCreate<T>(T prefab)
            where T : Component, IPooledObject;

        T GetOrCreate<T>(T prefab, Vector3 position, Quaternion rotation)
            where T : Component, IPooledObject;

        T GetOrCreate<T>(GameObject prefab, Vector3 position, Quaternion rotation)
            where T : Component, IPooledObject;

        int GetInstancesCount(GameObject prefab);
    }
}