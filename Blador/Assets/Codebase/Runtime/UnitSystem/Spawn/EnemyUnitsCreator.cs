using System;
using Codebase.Runtime.UnitSystem.Data.Enemy;
using Codebase.Runtime.UnitSystem.Factory;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.UnitSystem.Spawn
{
    class EnemyUnitsCreator : MonoBehaviour
    {
        [SerializeField] private EnemyUnitData EnemyUnitData;
        private EnemyFactory _enemyFactory;

        public void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }      
        
        [ContextMenu("Create Enemy")]
        public void CreateEnemy()
        {
            var enemy = _enemyFactory.Create(EnemyUnitData, transform.position, Quaternion.identity);
        }
    }
}