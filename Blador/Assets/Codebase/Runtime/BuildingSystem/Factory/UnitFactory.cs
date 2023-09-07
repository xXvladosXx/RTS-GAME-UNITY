using System;
using System.Threading.Tasks;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Infrastructure.AssetService;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem.Data.Enemy;
using Codebase.Runtime.UnitSystem.Spawn;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem.Factory
{
    public abstract class UnitFactory<TUnitView, TUnit, TUnitData> : IUnitsCreator
        where TUnitView : UnitView
        where TUnit : Unit
        where TUnitData : UnitData
    {
        protected readonly IObjectPool ObjectPool;
        protected readonly IAssetProvider AssetProvider;
        protected readonly ILevelBinder LevelBinder;

        public UnitFactory(IObjectPool objectPool,
            IAssetProvider assetProvider,
            ILevelBinder levelBinder)
        {
            ObjectPool = objectPool;
            AssetProvider = assetProvider;
            LevelBinder = levelBinder;
        }

        public async UniTask<(TUnitView unitView, TUnit unit)> Create(TUnitData unitData, Vector3 position, Quaternion rotation)
        {
            var unitView = ObjectPool.GetOrCreate<TUnitView>(unitData.Prefab, position, rotation);
            var unit = await SetupUnit(unitView, unitData);
            return (unitView, unit);
        }

        public abstract UniTask<TUnit> SetupUnit(TUnitView view, TUnitData allyUnitData);
    }
}