using UnityEngine;

namespace Codebase.Runtime.UnitSystem
{
    public interface ISelectable
    {
        SelectableData Data { get; }
        Vector3 Position { get; }
        Collider Collider { get; }
        bool IsSelected { get; }
        bool PossibleToControl { get; }
        void Select();
        void Deselect();
    }
}