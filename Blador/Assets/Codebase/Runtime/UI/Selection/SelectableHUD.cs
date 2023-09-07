using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.Selection;
using Codebase.Runtime.Selection.Actions;
using Codebase.Runtime.UI.Selection.Action;
using Codebase.Runtime.UnitSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Codebase.Runtime.UI.Selection
{
    public class SelectableHUD : SerializedMonoBehaviour
    {
        [SerializeField] private SelectableInfo _selectableInfo;
        [SerializeField] private Selectable _selectablePrefab;
        [SerializeField] private Transform _selectablesParent;
        
        [SerializeField] private CanvasGroup _selectablesCanvasGroup;
        [SerializeField] private CanvasGroup _selectableInfoCanvasGroup;
    
        [SerializeField] private PossibleActions _possibleActions;
        
        private Dictionary<ISelectable, Selectable> _selectables = new();
        private SelectableCollector _selectableCollector;
        private IObjectPool _objectPool;
        private UIQueue _uiQueue;

        [Inject]
        public void Construct(SelectableCollector selectableCollector,
            IObjectPool objectPool,
            UIQueue uiQueue)
        {
            _selectableCollector = selectableCollector;
            _objectPool = objectPool;
            _uiQueue = uiQueue;
        }

        private void OnEnable()
        {
            _selectableCollector.OnSelectableAdded += OnSelectableAdded;
            _selectableCollector.OnSelectableRemoved += OnSelectableRemoved;
        }

        private void OnDisable()
        {
            _selectableCollector.OnSelectableAdded -= OnSelectableAdded;
            _selectableCollector.OnSelectableRemoved -= OnSelectableRemoved;
        }

        private void OnSelectableAdded(ISelectable entity)
        {
            if (_selectableCollector.SelectedEntities.Count == 1)
            {
                ActivateInfoOneUnit(entity);

                var selectable = _objectPool.GetOrCreate(_selectablePrefab);
                selectable.Activate(entity.Data);
                _selectables.Add(entity, selectable);
                selectable.transform.SetParent(_selectablesParent);
            }
            else
            {
                _selectableInfo.Deactivate();
                _selectableInfoCanvasGroup.alpha = 0;
                
                var selectable = _objectPool.GetOrCreate(_selectablePrefab);
                _selectables.Add(entity, selectable);
                selectable.transform.SetParent(_selectablesParent);
                selectable.Activate(entity.Data);
                _selectablesCanvasGroup.alpha = 1;
                _possibleActions.Deactivate();
            }
        }

        private void ActivateInfoOneUnit(ISelectable entity)
        {
            _selectablesCanvasGroup.alpha = 0;
            _selectableInfoCanvasGroup.alpha = 1;
            _selectableInfo.ActivateDetailInfo(entity.Data);
            _possibleActions.Activate(entity.Data);
            _uiQueue.PushPage(_possibleActions);
        }

        private void OnSelectableRemoved(ISelectable entity)
        {
            _selectables[entity].Deactivate();
            
            if (_selectableCollector.SelectedEntities.Count == 1)
            {
                ActivateInfoOneUnit(entity);
            }
            
            _selectables.Remove(entity);
            
            if (_selectableCollector.SelectedEntities.Count == 0)
            {
                _selectableInfoCanvasGroup.alpha = 0;
                _selectablesCanvasGroup.alpha = 0;
                _selectableInfo.Deactivate();
                _selectables.Clear();
                
            }
        }
    }
}