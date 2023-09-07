using UnityEngine;

namespace Codebase.Runtime.Utils
{
    public static class TargetExtension
    {
        public static bool IsNullOrMissing<T>(this T obj)
        {
            return obj == null || (obj is MonoBehaviour mb && mb.FilterMissingReference() == null);
        }
        
        public static MonoBehaviour FilterMissingReference(this MonoBehaviour obj)
        {
            return (obj != null && obj.isActiveAndEnabled) ? obj : null;
        }
    }
}