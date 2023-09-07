using UnityEngine;

namespace Codebase.Runtime.UI
{
    public interface IQueryableUIElement
    {
        public void Activate();
        public void Deactivate();
    }

    class QueryableUIElement : MonoBehaviour, IQueryableUIElement
    {
        public void Activate()
        {
            
        }

        public void Deactivate()
        {
        }
    }
}