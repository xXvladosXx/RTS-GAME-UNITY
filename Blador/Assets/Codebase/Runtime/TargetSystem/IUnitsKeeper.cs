using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.KDTree;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Runtime.TargetSystem
{
    public interface IUnitsKeeper : IGameLoop 
    {
        UnitView FindClosestUnit(Team team, Vector3 position);
        Unit FindUnitByView(UnitView unitView);
        void OnUnitCreated(UnitView unitView, Unit unit);
        KdTree<UnitView> FindProperTeam(Team team);
        Team GetOpponentsTeam(Team team);
    }
}