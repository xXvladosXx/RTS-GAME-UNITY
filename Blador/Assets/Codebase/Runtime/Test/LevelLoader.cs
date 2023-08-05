using Codebase.Runtime.Infrastructure.StateMachine;
using Codebase.Runtime.Infrastructure.StateMachine.States;
using Zenject;

namespace Codebase.Runtime.Test
{
    public class LevelLoader : IInitializable
    {
        private readonly IGameStateMachine _stateMachine;

        public LevelLoader(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _stateMachine.Enter<LoadLevelState, string>("Main");
        }
    }
}