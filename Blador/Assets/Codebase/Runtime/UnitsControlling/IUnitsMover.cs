using System.Collections.Generic;
using Codebase.Runtime.UnitSystem;
using UnityEngine;

namespace Codebase.Runtime.UnitsControlling
{
    public interface IUnitsMover
    {
        void MoveUnits(HashSet<ISelectable> selectables, Vector3 destination);
    }
}