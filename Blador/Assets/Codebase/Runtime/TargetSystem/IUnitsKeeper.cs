using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Runtime.TargetSystem
{
    public interface IUnitsKeeper : IGameLoop
    {
        UnitView FindClosestUnit(Team team, Vector3 position);
        void Initialize();
    }
}