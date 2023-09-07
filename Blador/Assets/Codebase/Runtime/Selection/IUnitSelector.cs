using UnityEngine;

namespace Codebase.Runtime.Selection.NewSelection
{
    public interface IUnitSelector
    {
        void OnStartSelect(Vector3 position);
        void OnSelecting(Vector3 position);
    }
}