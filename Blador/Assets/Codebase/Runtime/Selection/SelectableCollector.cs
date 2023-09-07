using System;
using System.Collections.Generic;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.UnitSystem;

namespace Codebase.Runtime.Selection
{
    public class SelectableCollector
    {
        public readonly List<ISelectable> AvailableEntities = new();
        private readonly HashSet<ISelectable> _selectedEntities = new();

        public HashSet<ISelectable> SelectedEntities => _selectedEntities;
        
        public event Action<ISelectable> OnSelectableAdded;
        public event Action<ISelectable> OnSelectableRemoved;

        public SelectableCollector(ILevelBinder levelBinder)
        {
            levelBinder.SelectableCollector = this;
        }
        
        public void AddAvailableEntity(ISelectable selectable)
        {
            AvailableEntities.Add(selectable);
        }
        
        public void RemoveAvailableEntity(ISelectable selectable)
        {
            AvailableEntities.Remove(selectable);
        }
        
        public void ClearAvailableEntities()
        {
            AvailableEntities.Clear();
        }
        
        public void AddSelected(ISelectable selectable)
        {
            _selectedEntities.Add(selectable);
            selectable.Select();
            OnSelectableAdded?.Invoke(selectable);
        }
        
        public void RemoveSelected(ISelectable selectable)
        {
            _selectedEntities.Remove(selectable);
            selectable.Deselect();
            OnSelectableRemoved?.Invoke(selectable);
        }
        
        public void DeselectAll()
        {
            foreach (var entity in _selectedEntities)
            {
                entity.Deselect();
            }
            _selectedEntities.Clear();
        }

        public bool IsSelected(ISelectable selectable) => 
            _selectedEntities.Contains(selectable);

        public HashSet<ISelectable> GetControlledSelectables()
        {
            var controlledUnits = new HashSet<ISelectable>();
            foreach (var selectedEntity in _selectedEntities)
            {
                if(selectedEntity.PossibleToControl)
                    controlledUnits.Add(selectedEntity);
            }

            return controlledUnits;
        }
    }
}