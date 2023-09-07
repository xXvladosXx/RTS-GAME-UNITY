using System.Collections.Generic;
using System.Linq;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Infrastructure.AssetService;
using Codebase.Runtime.Infrastructure.SceneManagement;
using Codebase.Runtime.Infrastructure.StaticData;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem.Factory;
using Codebase.Runtime.UnitSystem.Spawn;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Runtime.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IObjectPool _objectPool;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly List<IUnitsCreator> _unitsCreators;
        private readonly ILevelBinder _levelBinder;

        public GameFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IObjectPool objectPool,
            ICoroutineRunner coroutineRunner,
            List<IUnitsCreator> unitsCreators,
            ILevelBinder levelBinder)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _objectPool = objectPool;
            _coroutineRunner = coroutineRunner;
            _unitsCreators = unitsCreators;
            _levelBinder = levelBinder;
        }

        public async UniTask<IUnitsCreator> CreateUnitsCreator()
        {
            var animal = await _assetProvider.Load<GameObject>("EnemyCreator");
            var spawner = Object.Instantiate(animal).GetComponent<EnemyUnitsCreator>();
            var enemyFactory = _unitsCreators.FirstOrDefault(item => item is EnemyFactory) as EnemyFactory;
            spawner.Construct(enemyFactory, _levelBinder);
            
            spawner.CreateEnemy();

            return null;
        }
        
        public async UniTask<IUnitsCreator> CreateAllyUnitsCreator()
        {
            var animal = await _assetProvider.Load<GameObject>("AllyCreator");
            var spawner = Object.Instantiate(animal).GetComponent<AllyUnitsCreator>();
            _levelBinder.SelectableCollector.AddAvailableEntity(spawner);
            var enemyFactory = _unitsCreators.FirstOrDefault(item => item is AllyFactory) as AllyFactory;
            spawner.Construct(enemyFactory, _levelBinder);

            spawner.CreateEnemy();
            
            return null;
        }
    }
}