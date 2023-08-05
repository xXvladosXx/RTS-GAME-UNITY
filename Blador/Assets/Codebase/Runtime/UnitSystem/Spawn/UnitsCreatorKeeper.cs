using System;
using System.Collections.Generic;
using System.Linq;
using Codebase.Runtime.UnitSystem.Factory;

namespace Codebase.Runtime.UnitSystem.Spawn
{
    public class UnitsCreatorKeeper  : IUnitsCreatorKeeper
    {
        private List<IUnitsCreator> _unitsCreators = new();
        private Dictionary<Type, IEnumerable<IUnitsCreator>> _cachedUnitsCreators = new();

        public T GetOne<T>() => GetCachedValues<T>().FirstOrDefault();
        public IEnumerable<T> GetAll<T>() => GetCachedValues<T>();

        private IEnumerable<T> GetCachedValues<T>()
        {
            if (!_cachedUnitsCreators.ContainsKey(typeof(T)))
            {
                var found = _unitsCreators.OfType<T>();
                var enumerable = found as T[] ?? found.ToArray();
                if (enumerable.Any())
                    _cachedUnitsCreators.Add(typeof(T), enumerable.Cast<IUnitsCreator>());
            }

            if (_cachedUnitsCreators.TryGetValue(typeof(T), out var values))
                return values.Cast<T>();

            return Enumerable.Empty<T>();
        }
        
        public void Add(IUnitsCreator unitCreator)
        {
            _unitsCreators.Add(unitCreator);
        }
        
        public void Remove(IUnitsCreator unitCreator)
        {
            _unitsCreators.Remove(unitCreator);
        }

        public void Dispose()
        {
            _unitsCreators.Clear();
        }
    }
}