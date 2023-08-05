using Codebase.Runtime.Infrastructure.StateMachine;
using Codebase.Runtime.Infrastructure.StateMachine.States;
using Codebase.Runtime.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Runtime.Installer
{
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<IGameStateMachine, BootstrapState, BootstrapState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadLevelState, LoadLevelState.Factory>();
            Container.BindFactory<IGameStateMachine, LoadProgressState, LoadProgressState.Factory>();
            Container.BindFactory<IGameStateMachine, GameLoopState, GameLoopState.Factory>();
            Container.BindFactory<IGameStateMachine, GamePausedState, GamePausedState.Factory>();
            Container.BindFactory<IGameStateMachine, GameOverState, GameOverState.Factory>();
            
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}