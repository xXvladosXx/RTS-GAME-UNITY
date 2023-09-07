using System;
using System.Threading.Tasks;
using Codebase.Runtime.DamageSystem;
using Codebase.Runtime.DamageSystem.Weapon;
using Codebase.Runtime.DamageSystem.Weapon.Range;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Infrastructure.AssetService;
using Codebase.Runtime.Movement;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem.Data.Enemy;
using Codebase.Runtime.UnitSystem.Spawn;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem.Factory
{
    public class EnemyFactory : UnitFactory<UnitView, EnemyUnit, EnemyUnitData>
    {
        private readonly GameLoopHandler _gameLoopHandler;

        public EnemyFactory(IObjectPool objectPool,
            GameLoopHandler gameLoopHandler,
            IAssetProvider assetProvider,
            ILevelBinder levelBinder) : base(objectPool, assetProvider, levelBinder)
        {
            _gameLoopHandler = gameLoopHandler;
        }

        public override async UniTask<EnemyUnit> SetupUnit(UnitView view, EnemyUnitData unitData)
        {
            var data = await AssetProvider.Load<ScriptableObject>(unitData.AttackData) as RangeWeaponData;
            var gameObject = await AssetProvider.Load<GameObject>(data.ProjectilePrefab);
            var projectile = gameObject.GetComponent<Projectile>();

            var movement = new UnitMovement(view.NavMeshAgent, unitData.Stats.Speed);
            var weapon = new Weapon<WeaponData>(new ProjectileAttackType(ObjectPool,projectile), data);
            var attack = new UnitAttackComponent(unitData.Stats.AttackSpeed, unitData.Stats.AttackRange, view, weapon, view.AnimationStateReader);
            var enemyUnit = new EnemyUnit(view, LevelBinder.TargetsProvider, movement, attack);
            
            enemyUnit.Initialize();
            
            _gameLoopHandler.Add(enemyUnit);
            LevelBinder.SelectableCollector.AddAvailableEntity(enemyUnit.UnitView);

            return enemyUnit;
        }
    }
}