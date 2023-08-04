using System.Collections;
using UnityEngine;

namespace Codebase.Runtime.Infrastructure.SceneManagement
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine routine);
    }
}