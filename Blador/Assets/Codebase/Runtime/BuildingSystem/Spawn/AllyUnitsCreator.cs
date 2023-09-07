using Codebase.Runtime.BuildingSystem;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem.Data.Enemy;
using Codebase.Runtime.UnitSystem.Factory;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Codebase.Runtime.UnitSystem.Spawn
{
    public class AllyUnitsCreator : BuildingView
    {
        [SerializeField] private AllyUnitData allyUnitData;
        private AllyFactory _allyFactory;
        private ILevelBinder _levelBinder;

        public void Construct(AllyFactory allyFactory, 
            ILevelBinder levelBinder)
        {
            _allyFactory = allyFactory;
            _levelBinder = levelBinder;
        }      
        
        [Button]
        public async void CreateEnemy()
        {
            var enemy = await _allyFactory.Create(allyUnitData, transform.position, Quaternion.identity);
            
            _levelBinder.UnitsKeeper.OnUnitCreated(enemy.Item1, enemy.Item2);
            _levelBinder.TargetsProvider.OnTargetCreated(enemy.Item1, enemy.Item1.Team);
        }
    }
}