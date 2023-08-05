using Codebase.Runtime.Infrastructure.SceneManagement;
using Codebase.Runtime.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Runtime.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Exit()
        {
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("Main");
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, BootstrapState>
        {
        }
    }
}