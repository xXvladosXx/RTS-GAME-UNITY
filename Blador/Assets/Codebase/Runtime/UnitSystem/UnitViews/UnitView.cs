using System;
using Codebase.Runtime.PoolSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Codebase.Runtime.UnitSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitView : MonoBehaviour, ISelectable, IPooledObject
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [SerializeField] private GameObject _selectionIndicator;
        public bool IsSelected { get; private set; }
        
        public event Action<Component> OnDestroyAsPooledObject;

        public int PrefabInstanceID { get; set; }

        private void OnValidate()
        {
            if(NavMeshAgent == null)
                NavMeshAgent = GetComponent<NavMeshAgent>();
        }

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