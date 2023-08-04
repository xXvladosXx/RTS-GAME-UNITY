using UnityEngine;

namespace Codebase.Runtime.Infrastructure.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
    }
}