using System;
using Cysharp.Threading.Tasks;

namespace Codebase.Runtime.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        string GetCurrentScene { get; }
        UniTask Load(string name, Action onLoaded = null);
    }
}