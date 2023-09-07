using Codebase.Runtime.DamageSystem.Weapon.Range;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Codebase.Runtime.DamageSystem.Weapon
{
    [CreateAssetMenu(fileName = "RangeWeaponData", menuName = "Attack Data/Range Weapon Data", order = 0)]
    public class RangeWeaponData : WeaponData 
    {
        public AssetReference ProjectilePrefab;
    }
}