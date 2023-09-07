using System;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.BuildingSystem
{
    public class BuildingView : MonoBehaviour, ITarget, ISelectable
    {
        [field: SerializeField] public SelectableData Data { get; private set; }

        [Header("Building parameters")]
        [SerializeField] protected GameObject _selectionCircle;
        [SerializeField] private Collider _collider;
        public Vector3 Position => transform.position;
        public Collider Collider => _collider;
        public bool IsSelected { get; private set; }
        public bool PossibleToControl { get; }

        private void Awake()
        {
            _selectionCircle.SetActive(false);
        }

        public void Select()
        {
            IsSelected = true;
            _selectionCircle.SetActive(true);
        }

        public void Deselect()
        {
            IsSelected = false;
            _selectionCircle.SetActive(false);
        }
    }
}