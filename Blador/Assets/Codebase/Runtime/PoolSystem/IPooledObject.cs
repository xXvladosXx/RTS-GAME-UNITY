using System;
using UnityEngine;

namespace Codebase.Runtime.PoolSystem
{
    public interface IPooledObject
    {
        event Action<Component> OnDestroyAsPooledObject;

        int PrefabInstanceID { get; set; }

        void DestroyAsPooledObject();
    }
}