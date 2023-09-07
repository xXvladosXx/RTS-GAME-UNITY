using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Runtime.UnitSystem
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "RTS/UnitData")]
    public class UnitData : SelectableData
    {
        public string UnitName;
        public GameObject Prefab;
        public AssetReference AttackData;
    }
}