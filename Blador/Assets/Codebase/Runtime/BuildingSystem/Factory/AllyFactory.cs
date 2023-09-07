using System;
using System.Threading.Tasks;
using Codebase.Runtime.DamageSystem;
using Codebase.Runtime.DamageSystem.Weapon;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Infrastructure.AssetService;
using Codebase.Runtime.Movement;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem.Data.Enemy;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem.Factory
{
    public class AllyFactory : UnitFactory<AllyView, AllyUnit, AllyUnitData>
    {
        private readonly GameLoopHandler _gameLoopHandler;
        
        public AllyFactory(IObjectPool objectPool,
            IAssetProvider assetProvider,
            GameLoopHandler gameLoopHandler,
            ILevelBinder levelBinder) : base(objectPool, assetProvider, levelBinder)
        {
            _gameLoopHandler = gameLoopHandler;
        }

        public override async UniTask<AllyUnit> SetupUnit(AllyView view, AllyUnitData unitData)
        {
            var data = await AssetProvider.Load<ScriptableObject>(unitData.AttackData) as WeaponData;

            var movement = new UnitMovement(view.NavMeshAgent, unitData.Stats.Speed);
            var weapon = new Weapon<WeaponData>(new SingleAttackType(), data);
            var attack = new UnitAttackComponent(unitData.Stats.AttackSpeed, unitData.Stats.AttackRange, view, weapon, view.AnimationStateReader);
            var allyUnit = new AllyUnit(view, LevelBinder.TargetsProvider, movement, attack);
            
            allyUnit.Initialize();

            _gameLoopHandler.Add(allyUnit);
            LevelBinder.SelectableCollector.AddAvailableEntity(allyUnit.UnitView);
            
            return allyUnit;
        }
    }
}