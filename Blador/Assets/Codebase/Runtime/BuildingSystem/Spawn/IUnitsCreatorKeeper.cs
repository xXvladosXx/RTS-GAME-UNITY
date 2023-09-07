using System.Collections.Generic;

namespace Codebase.Runtime.UnitSystem.Spawn
{
    public interface IUnitsCreatorKeeper
    {
        IEnumerable<T> GetAll<T>();
        T GetOne<T>();
    }
}