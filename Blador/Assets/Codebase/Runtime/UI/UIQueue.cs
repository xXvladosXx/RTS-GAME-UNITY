using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Runtime.UI
{
    public class UIQueue 
    {
        private Stack<IQueryableUIElement> _queue = new Stack<IQueryableUIElement>();
        
        public void PushPage(IQueryableUIElement element)
        {
            element.Activate();

            if (_queue.Count > 0)
            {
                var currentPage = _queue.Peek();
                currentPage.Deactivate();
            }

            _queue.Push(element);
        }
        
        public void PopPage()
        {
            if (_queue.Count > 1)
            {
                var page = _queue.Pop();
                page.Deactivate();

                var newCurrentPage = _queue.Peek();
                newCurrentPage.Activate();
            }
            else
            {
                Debug.LogWarning("Trying to pop a page but only 1 page remains in the stack!");
            }
        }
    }
}