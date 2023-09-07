using System;
using System.Collections.Generic;

namespace Codebase.Runtime.Selection.Actions
{
    [Serializable]
    public class SpawnData : ActionData
    {
        public List<string> SpawnedUnits = new();
    }
}