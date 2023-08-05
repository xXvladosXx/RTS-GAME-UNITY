using System;
using Codebase.Runtime.PoolSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Codebase.Runtime.UnitSystem
{
    public abstract class UnitView : MonoBehaviour, ISelectable, IPooledObject
    {
        [SerializeField] private GameObject _selectionIndicator;
        
        public bool IsSelected { get; private set; }
        
        public event Action<Component> OnDestroyAsPooledObject;

        public int PrefabInstanceID { get; set; }

        public void SetSelected(bool selected)
        {
            IsSelected = selected;
            _selectionIndicator.SetActive(selected);
        }

        public void DestroyAsPooledObject()
        {
            
        }
    }
}