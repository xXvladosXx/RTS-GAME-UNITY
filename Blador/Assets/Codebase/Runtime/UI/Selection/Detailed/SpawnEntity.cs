using System;
using Codebase.Runtime.PoolSystem;
using UnityEngine;

namespace Codebase.Runtime.UI.Selection.Detailed
{
    public class SpawnEntity : MonoBehaviour, IPooledObject
    {
        public event Action<Component> OnDestroyAsPooledObject;
        public int PrefabInstanceID { get; set; }
        public void DestroyAsPooledObject() => OnDestroyAsPooledObject?.Invoke(this);
    }
}