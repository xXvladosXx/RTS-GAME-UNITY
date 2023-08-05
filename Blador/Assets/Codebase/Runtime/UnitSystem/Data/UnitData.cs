using UnityEngine;

namespace Codebase.Runtime.UnitSystem
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "RTS/UnitData")]
    public class UnitData : ScriptableObject
    {
        public string UnitName;
        public GameObject Prefab;
        public UnitStats Stats;
    }
}