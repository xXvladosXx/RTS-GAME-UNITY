using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.Runtime.UI.Selection.Action
{
    public class DetailedActions : MonoBehaviour, IQueryableUIElement
    {
        [SerializeField] private Button _backButton;
        private UIQueue _uiQueue;

        [Inject]
        public void Construct(UIQueue uiQueue)
        {
            _uiQueue = uiQueue;
        }
        
        private void Awake()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }

        private void OnBackButtonClicked()
        {
            _uiQueue.PopPage();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            
        }
    }
}