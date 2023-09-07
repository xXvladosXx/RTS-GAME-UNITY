using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Infrastructure;
using Codebase.Runtime.Infrastructure.AssetService;
using Codebase.Runtime.Infrastructure.Factory;
using Codebase.Runtime.Infrastructure.SceneManagement;
using Codebase.Runtime.Infrastructure.StateMachine;
using Codebase.Runtime.Infrastructure.StaticData;
using Codebase.Runtime.InputSystem;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UnitSystem;
using Codebase.Runtime.UnitSystem.Factory;
using Codebase.Runtime.UnitSystem.Spawn;
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

            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IObjectPool>().To<ObjectPool>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameLoopHandler>().AsSingle();

            Container.Bind<IUnitsCreator>().To<EnemyFactory>().AsTransient();
            Container.Bind<IUnitsCreator>().To<AllyFactory>().AsTransient();

            Container.Bind<ILevelBinder>().To<LevelBinder>().AsSingle();
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