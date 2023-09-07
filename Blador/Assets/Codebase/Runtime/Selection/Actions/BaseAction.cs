using System;
using UnityEngine.AddressableAssets;

namespace Codebase.Runtime.Selection.Actions
{
    [Serializable]
    public abstract class BaseAction
    {
        public ActionData Data;
        public Action Action;
        public AssetReferenceSprite Icon;
    }
}