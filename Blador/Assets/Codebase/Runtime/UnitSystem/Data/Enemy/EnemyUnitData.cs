using UnityEngine;

namespace Codebase.Runtime.UnitSystem.Data.Enemy
{
    [CreateAssetMenu(fileName = "EnemyUnitData", menuName = "RTS/Enemy")]
    public class EnemyUnitData : UnitData
    {
        public int GoldReward;
        public int ExpReward;
    }
}