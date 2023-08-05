using Codebase.Runtime.Infrastructure.Factory;
using Codebase.Runtime.Infrastructure.StateMachine.States.Core;
using Codebase.Runtime.InputSystem;
using Zenject;

namespace Codebase.Runtime.Infrastructure.StateMachine.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IInputProvider _inputProvider;

        public GameLoopState(IGameStateMachine gameStateMachine,
            IGameFactory gameFactory,
            IInputProvider inputProvider)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _inputProvider = inputProvider;
        }
        
        public void Enter()
        {
          _inputProvider.EnablePlayer();
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}