using System;
using System.Collections.Generic;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.KDTree;
using Codebase.Runtime.UnitSystem;
using Codebase.Runtime.UnitSystem.Spawn;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.TargetSystem
{
    public class UnitsKeeper : IUnitsKeeper
    {
        private readonly IUnitsCreatorKeeper _unitsCreatorKeeper;
        private readonly GameLoopHandler _gameLoopHandler;

        private readonly Dictionary<UnitView, Unit> _unitsViews = new();

        private readonly Dictionary<Team, KdTree<UnitView>> _units = new()
        {
            {Team.Allies, new KdTree<UnitView>()},
            {Team.Enemies, new KdTree<UnitView>()}
        };
        
        public UnitsKeeper(IUnitsCreatorKeeper unitsCreatorKeeper,
            ILevelBinder levelBinder,
            GameLoopHandler gameLoopHandler)
        {
            _unitsCreatorKeeper = unitsCreatorKeeper;
            levelBinder.UnitsKeeper = this;
            gameLoopHandler.Add(this);
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
            _units.Clear();
        }

        public void OnUnitCreated(UnitView unitView, Unit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));
            
            _unitsViews.Add(unitView, unit);
            _units[unitView.Team].Add(unit.UnitView);
        }

        public Unit FindUnitByView(UnitView unitView)
        {
            _unitsViews.TryGetValue(unitView, out var unit);
            return unit;
        }

        public UnitView FindClosestUnit(Team team, Vector3 position)
        {
            var teamOpponents = GetOpponentsTeam(team);
            var properTeam = FindProperTeam(teamOpponents);
            return properTeam?.FindClosest(position);
        }

        public KdTree<UnitView> FindProperTeam(Team team)
        {
            _units.TryGetValue(team, out var possibleUnits);
            return possibleUnits;
        }

        public Team GetOpponentsTeam(Team team)
        {
            return team == Team.Enemies ? Team.Allies : Team.Enemies;
        }
    }
}