using System;

namespace Codebase.Runtime.UnitSystem.Spawn
{
    public interface IUnitsCreator
    {
        event Action<Unit<UnitView>> OnUnitCreated;
    }
}