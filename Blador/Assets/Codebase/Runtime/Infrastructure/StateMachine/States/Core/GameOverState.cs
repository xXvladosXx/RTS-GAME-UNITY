using UnityEngine;
using Zenject;

namespace Codebase.Runtime.Infrastructure.StateMachine.States.Core
{
    public class GameOverState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public GameOverState(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            Debug.Log("Congratulations! You won!");
        }

        private void RestartLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("Gameplay");
        }

        public void Exit()
        {
            
        }
        
        public class Factory : PlaceholderFactory<IGameStateMachine, GameOverState>
        {
        }
    }
}