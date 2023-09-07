namespace Codebase.Runtime.AnimatorSystem
{
    public interface IAnimationStateReader
    {
        void PlayAnimation(int hash, bool value);
        void EnteredState(int stateHash);
        void UpdateState();
        void ExitedState(int stateHash);
    }
}