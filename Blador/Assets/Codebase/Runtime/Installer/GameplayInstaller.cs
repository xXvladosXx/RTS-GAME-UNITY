using Codebase.Runtime.CameraSystem;
using Codebase.Runtime.CameraSystem.Factory;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.Selection;
using Codebase.Runtime.Selection.NewSelection;
using Codebase.Runtime.TargetSystem;
using Codebase.Runtime.UI;
using Codebase.Runtime.UnitsControlling;
using Codebase.Runtime.UnitSystem;
using Codebase.Runtime.UnitSystem.Spawn;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.Installer
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplayCanvas _gameplayCanvas;
        
        public override void InstallBindings()
        {
            Container.Bind<GameplayCanvas>().FromInstance(_gameplayCanvas).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraFacade>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitsCreatorKeeper>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitsKeeper>().AsSingle();
            Container.BindInterfacesAndSelfTo<TargetsProvider>().AsSingle().NonLazy();

            Container.Bind<SelectableCollector>().AsSingle().NonLazy();
            Container.Bind<IRaycastHandler>().To<RaycastHandler>().AsSingle();
            
            Container.Bind<IUnitsMover>().To<UnitsMover>().AsSingle();
            Container.Bind<IUnitsAttacker>().To<UnitsAttacker>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<SelectedUnitsController>().AsSingle();
            Container.BindInterfacesAndSelfTo<RectSelectionController>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitSelector>().AsSingle();
            Container.Bind<IUnitFormation>().To<UnitFormation>().AsSingle();
            Container.Bind<UIQueue>().AsSingle();
        }
    }
}