namespace Codebase.Runtime.GameplayCore
{
    public interface IGameLoop
    {
        public bool GameUpdate();
        public void Recycle();
    }
}