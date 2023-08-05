using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Runtime.PoolSystem
{
    class ObjectPool : MonoBehaviour, IObjectPool
    {
        private readonly Dictionary<int, Stack<Object>> _pool = new();
        
        public T GetOrCreate<T>(T prefab) where T : Component, IPooledObject => 
            GetOrCreate(prefab, Vector3.zero, Quaternion.identity);

        public T GetOrCreate<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component, IPooledObject => 
            GetOrCreate<T>(prefab.gameObject, position, rotation);

        public T GetOrCreate<T>(GameObject prefab, Vector3 position, Quaternion rotation) where T : Component, IPooledObject
        {
            var id = prefab?.GetInstanceID() ?? throw new ArgumentNullException(nameof(prefab));
            var objects = GetObjects(id);

            T instance;

            if (objects.Count > 0)
            {
                instance = (T)objects.Pop();
                instance.transform.position = position;
                instance.transform.rotation = rotation;
                instance.gameObject.SetActive(true);
            }
            else
            {
                var newObject = Instantiate(prefab, position, rotation, transform);
                instance = newObject.GetComponent<T>() ?? newObject.AddComponent<T>();
                instance.OnDestroyAsPooledObject += OnDestroyHandler;
                instance.PrefabInstanceID = id;
            }

            return instance ? instance : throw new NullReferenceException(nameof(instance));
        }

        public int GetInstancesCount(GameObject prefab)
        {
            var id = prefab?.GetInstanceID() ?? throw new ArgumentNullException(nameof(prefab));
            return !_pool.TryGetValue(id, out var objects) ? 0 : objects.Count;
        }
        
        private void OnDestroyHandler(Component returned)
        {
            var id = (returned as IPooledObject)?.PrefabInstanceID ?? throw new ArgumentException(nameof(returned));
            var objects = GetObjects(id);
            objects.Push(returned);
            returned.gameObject.SetActive(false);
            returned.transform.SetParent(transform);
        }
        
        private Stack<Object> GetObjects(int prefabInstanceID)
        {
            if (_pool.TryGetValue(prefabInstanceID, out var objects)) 
                return objects;
            
            objects = new Stack<Object>();
            _pool.Add(prefabInstanceID, objects);

            return objects;
        }
    }
}