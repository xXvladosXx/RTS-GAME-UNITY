using System.Collections.Generic;
using Codebase.Runtime.UnitSystem.Spawn;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Codebase.Runtime.Infrastructure.Factory
{
    public interface IGameFactory
    {
        UniTask<IUnitsCreator> CreateUnitsCreator();
        UniTask<IUnitsCreator> CreateAllyUnitsCreator();
    }
}