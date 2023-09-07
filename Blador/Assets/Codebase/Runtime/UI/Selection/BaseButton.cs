using System;
using Codebase.Runtime.Selection.Actions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Codebase.Runtime.UI.Selection
{
    public abstract class BaseButton : SerializedMonoBehaviour 
    {
        public abstract void Activate(BaseAction action);
        public abstract void Deactivate();
    }

}