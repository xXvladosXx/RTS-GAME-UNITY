using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Runtime.DamageSystem.Weapon
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "Attack Data", order = 0)]
    public class WeaponData : SerializedScriptableObject
    {
        public IAttackApplyForm AttackApplyForm;
        public DamageData DamageData;
    }
}