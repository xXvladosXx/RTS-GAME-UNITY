using Codebase.Runtime.Infrastructure.StateMachine.States.Core;
using Zenject;

namespace Codebase.Runtime.Infrastructure.StateMachine.States
{
    public class GamePausedState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GamePausedState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
           
        }

        private void OnPausePerformed()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
          
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GamePausedState>
        {
        }
    }
}