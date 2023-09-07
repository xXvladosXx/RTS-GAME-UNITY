using System.Collections.Generic;
using Codebase.Runtime.Selection.Actions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Runtime.UnitSystem
{
    [CreateAssetMenu(fileName = "SelectableData", menuName = "UnitSystem/SelectableData")]
    public class SelectableData : SerializedScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public Stats Stats;
        public List<BaseAction> Actions = new();
    }
}