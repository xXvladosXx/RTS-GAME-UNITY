using Codebase.Runtime.Infrastructure;
using Codebase.Runtime.Infrastructure.AssetService;
using Codebase.Runtime.Infrastructure.Factory;
using Codebase.Runtime.Infrastructure.SceneManagement;
using Codebase.Runtime.Infrastructure.StateMachine;
using Codebase.Runtime.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        
        public override void InstallBindings()
        {
            RegisterAssetProvider();
            RegisterStaticData();
            
            Container
                .Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefab(_gameBootstrapper);

            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentInNewPrefab(_coroutineRunner)
                .AsSingle();

            //Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }

        private void RegisterStaticData()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
        }

        private void RegisterAssetProvider()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        }
    }
}