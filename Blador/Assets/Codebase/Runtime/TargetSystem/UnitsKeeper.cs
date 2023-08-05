using System;
using System.Collections.Generic;
using Codebase.Runtime.KDTree;
using Codebase.Runtime.UnitSystem;
using Codebase.Runtime.UnitSystem.Spawn;
using UnityEngine;

namespace Codebase.Runtime.TargetSystem
{
    public class UnitsKeeper : IUnitsKeeper
    {
        private readonly IUnitsCreatorKeeper _unitsCreatorKeeper;
        private readonly Dictionary<Team, KdTree<UnitView>> _units = new();
        
        public UnitsKeeper(IUnitsCreatorKeeper unitsCreatorKeeper)
        {
            _unitsCreatorKeeper = unitsCreatorKeeper;
        }

        public void Initialize()
        {
            foreach (var unitsCreator in _unitsCreatorKeeper.GetAll<IUnitsCreator>())
            {
                unitsCreator.OnUnitCreated += OnUnitCreated;
            }
        }

        public bool GameUpdate()
        {
            foreach (var unit in _units)
            {
                unit.Value.UpdatePositions(2f);
            }
            return true;
        }

        public void Recycle()
        {
            foreach (var unitsCreator in _unitsCreatorKeeper.GetAll<IUnitsCreator>())
            {
                unitsCreator.OnUnitCreated -= OnUnitCreated;
            }
            
            _units.Clear();
        }

        private void OnUnitCreated(Unit<UnitView> unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));
            
            _units[unit.Team].Add(unit.UnitView);
        }

        public UnitView FindClosestUnit(Team team, Vector3 position)
        {
            var properTeam = FindProperTeam(team);
            return properTeam?.FindClosest(position);
        }

        private KdTree<UnitView> FindProperTeam(Team team)
        {
            _units.TryGetValue(team, out var possibleUnits);
            return possibleUnits;
        }
    }
}