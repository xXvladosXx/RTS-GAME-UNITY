using System;
using Codebase.Runtime.PoolSystem;
using Codebase.Runtime.UnitSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Runtime.UI.Selection
{
    public class Selectable : MonoBehaviour, IPooledObject
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _selectionImage;
        
        public void Activate(SelectableData entityData)
        {
            _nameText.text = entityData.Name;
            _selectionImage.sprite = entityData.Icon;
            _selectionImage.enabled = true;
        }

        public void Deactivate()
        {
            _selectionImage.enabled = false;
            DestroyAsPooledObject();
        }

        public event Action<Component> OnDestroyAsPooledObject;

        public int PrefabInstanceID { get; set; }

        public void DestroyAsPooledObject() => 
            OnDestroyAsPooledObject?.Invoke(this);
    }
}