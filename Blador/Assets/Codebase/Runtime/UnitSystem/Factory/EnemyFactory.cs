using System;
using Codebase.Runtime.Movement;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.UnitSystem.Data.Enemy;

namespace Codebase.Runtime.UnitSystem.Factory
{
    public class EnemyFactory : UnitFactory<UnitView, EnemyUnit, EnemyUnitData>
    {
        public EnemyFactory(IObjectPool objectPool) : base(objectPool) { }

        public override event Action<Unit<UnitView>> OnUnitCreated;

        public override EnemyUnit SetupUnit(UnitView unitView, EnemyUnitData unitData)
        {
            var movement = new UnitMovement(unitView.NavMeshAgent, unitData.Stats.Speed);
            var enemyUnit = new EnemyUnit(unitView, movement);

            OnUnitCreated?.Invoke(enemyUnit);

            return enemyUnit;
        }
    }
}