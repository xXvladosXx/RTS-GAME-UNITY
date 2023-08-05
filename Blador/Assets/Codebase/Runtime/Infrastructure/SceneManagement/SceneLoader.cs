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
                await SceneManager.LoadSceneAsync("Main");
            }   
            
            await UniTask.Delay(300);
            
//            await Addressables.LoadSceneAsync(name);
            
            onLoaded?.Invoke();
        }
    }
}