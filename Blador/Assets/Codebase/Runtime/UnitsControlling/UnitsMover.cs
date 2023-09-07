using System.Collections.Generic;
using Codebase.Runtime.Selection;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Runtime.UnitsControlling
{
    public class UnitsMover : IUnitsMover
    {
        private readonly IUnitsKeeper _unitsKeeper;
        private readonly IUnitFormation _unitFormation;

        public UnitsMover(IUnitsKeeper unitsKeeper,
            IUnitFormation unitFormation)
        {
            _unitsKeeper = unitsKeeper;
            _unitFormation = unitFormation;
        }
        
        public void MoveUnits(HashSet<ISelectable> selectables, Vector3 destination)
        {
            destination.y = 0;
            var units = new List<Unit>();
            
            foreach (var selectable in selectables)
            {
                if (selectable is UnitView unitView)
                {
                    var unit = _unitsKeeper.FindUnitByView(unitView);
                    unitView.Target = null;
                    units.Add(unit);
                }
            }

            _unitFormation.FormUnits(units, destination, 2f);
        }
    }
}