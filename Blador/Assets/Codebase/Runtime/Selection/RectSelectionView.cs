using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Runtime.Selection
{
    public class RectSelectionView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;

        public void SetPositions(Rect rect)
        {
            _rectTransform.sizeDelta = rect.size;
            _rectTransform.anchoredPosition = new Vector2(rect.x, rect.y);
        }
        
        public void SetVisible(bool isVisible)
        {
            _image.enabled = isVisible;
        }
    }
}