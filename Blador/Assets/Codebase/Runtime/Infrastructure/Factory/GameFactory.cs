using System.Collections.Generic;
using Codebase.Runtime.Infrastructure.AssetService;
using Codebase.Runtime.Infrastructure.SceneManagement;
using Codebase.Runtime.Infrastructure.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Codebase.Runtime.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;

        public GameFactory(IAssetProvider assetProvider,
            IStaticDataService staticDataService,
            ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
        }

        
    }
}