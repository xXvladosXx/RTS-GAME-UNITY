using System;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.UnitSystem.Spawn;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem.Factory
{
    public abstract class UnitFactory<TUnitView, TUnit, TUnitData> : IUnitsCreator
        where TUnitView : UnitView
        where TUnit : Unit<TUnitView>
        where TUnitData : UnitData
    {
        private readonly IObjectPool _objectPool;

        public abstract event Action<Unit<UnitView>> OnUnitCreated;

        public UnitFactory(IObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        public (TUnitView, TUnit) Create(TUnitData unitData, Vector3 position, Quaternion rotation)
        {
            var unitView = _objectPool.GetOrCreate<TUnitView>(unitData.Prefab, position, rotation);
            var unit = SetupUnit(unitView, unitData);
            return (unitView, unit);
        }

        public abstract TUnit SetupUnit(TUnitView unitView, TUnitData unitData);
    }
}