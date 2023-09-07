using Codebase.Runtime.UnitSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Runtime.UI.Selection
{
    public class SelectableInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Image _selectionImage;

        public void ActivateDetailInfo(SelectableData entityData)
        {
            _nameText.text = entityData.Name;
            _selectionImage.sprite = entityData.Icon;
            _selectionImage.enabled = true;
        }
        
        public void Deactivate()
        {
            _selectionImage.enabled = false;
        }
    }
}