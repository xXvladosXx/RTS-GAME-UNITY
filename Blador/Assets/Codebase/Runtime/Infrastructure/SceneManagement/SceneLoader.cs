using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Codebase.Runtime.Infrastructure.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        public string GetCurrentScene => SceneManager.GetActiveScene().name;

        public async UniTask Load(string name, Action onLoaded = null)
        {
            if (name == GetCurrentScene)
            {
                await Addressables.LoadSceneAsync("Main");
            }   
            
            await UniTask.Delay(30);
            
            var waitNextScene = Addressables.LoadSceneAsync(name);
            while (!waitNextScene.IsDone)
            {
                await UniTask.Yield();
            }
            
            onLoaded?.Invoke();
        }
    }
}