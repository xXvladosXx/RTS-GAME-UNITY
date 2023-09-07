using Cysharp.Threading.Tasks;

namespace Codebase.Runtime.Infrastructure.StateMachine.States.Core
{
    public interface ILoadState<in TLoad> : IExitableState
    {
        UniTask Load(TLoad save);
    }
}