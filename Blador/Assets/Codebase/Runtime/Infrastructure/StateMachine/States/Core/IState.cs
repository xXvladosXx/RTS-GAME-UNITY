namespace Codebase.Runtime.Infrastructure.StateMachine.States.Core
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}