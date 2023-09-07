using System;
using System.Collections.Generic;
using Codebase.Runtime.UnitSystem;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.UI.Selection.Action
{
    public class PossibleActions : MonoBehaviour, IQueryableUIElement
    {
        [SerializeField] private Dictionary<Codebase.Runtime.Selection.Actions.Action, BaseButton> _actionButtons = new();
        [SerializeField] private DetailedActions _detailedActions;

        private UIQueue _uiQueue;
    
        [Inject]
        public void Construct(UIQueue uiQueue)
        {
            _uiQueue = uiQueue;
        }

        private void OnEnable()
        {
            _detailedActions.gameObject.SetActive(false);
        }

        public void Activate(SelectableData selectableData)
        {
            foreach (var action in selectableData.Actions)
            {
                if (_actionButtons.TryGetValue(action.Action, out var button))
                {
                    button.Activate(action);
                }
            }
        }

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            _uiQueue.PopPage();
            foreach (var actionButton in _actionButtons)
            {
                actionButton.Value.Deactivate();
            }
        }
    }
}