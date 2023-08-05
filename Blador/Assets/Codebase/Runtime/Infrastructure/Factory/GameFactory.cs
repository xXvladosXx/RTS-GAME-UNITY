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
        
        private readonly UnitsCreatorKeeper _unitsCreatorKeeper;
        private readonly IUnitsKeeper _unitsKeeper;
        private readonly EnemyFactory _enemyUnitFactory;

        public GameFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            IObjectPool objectPool,
            ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _objectPool = objectPool;
            _coroutineRunner = coroutineRunner;
            
            _unitsCreatorKeeper = new UnitsCreatorKeeper();
            _enemyUnitFactory = new EnemyFactory(_objectPool);
            _unitsCreatorKeeper.Add(_enemyUnitFactory);

            _unitsKeeper = new UnitsKeeper(_unitsCreatorKeeper);
            _unitsKeeper.Initialize();
        }

        public async UniTask<IUnitsCreator> CreateUnitsCreator()
        {
            var animal = await _assetProvider.Load<GameObject>("EnemyCreator");
            var go = GameObject.Instantiate(animal);
            
            return null;
        }
    }
}