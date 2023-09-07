using System;
using Codebase.Runtime.BuildingSystem;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem.Data.Enemy;
using Codebase.Runtime.UnitSystem.Factory;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.UnitSystem.Spawn
{
    class EnemyUnitsCreator : BuildingView
    {
        [SerializeField] private EnemyUnitData EnemyUnitData;
        private EnemyFactory _enemyFactory;
        private ILevelBinder _levelBinder;

        public void Construct(EnemyFactory enemyFactory,
            ILevelBinder levelBinder)
        {
            _enemyFactory = enemyFactory;
            _levelBinder = levelBinder;
        }      
        
        [Button]
        public async void CreateEnemy()
        {
            var enemy = await _enemyFactory.Create(EnemyUnitData, transform.position, Quaternion.identity);
           
            _levelBinder.UnitsKeeper.OnUnitCreated(enemy.Item1, enemy.Item2);
            _levelBinder.TargetsProvider.OnTargetCreated(enemy.Item1, enemy.Item1.Team);
        }
    }
}