using Codebase.Runtime.Infrastructure.StateMachine;
using Codebase.Runtime.Infrastructure.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        private void Start()
        {
            _gameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
        
        public class Factory : PlaceholderFactory<GameBootstrapper>
        {
        }
    }
}