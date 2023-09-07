using System;
using Codebase.Runtime.InputSystem;
using Codebase.Runtime.Selection.NewSelection;
using Codebase.Runtime.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

namespace Codebase.Runtime.Selection
{
    public class RectSelectionController : IInitializable, ITickable, IDisposable
    {
        private readonly GameplayCanvas _gameplayCanvas;

        private readonly IInputProvider _input;
        private readonly IUnitSelector _unitSelector;

        private bool _isSelecting;
        private Vector2 _startPosition;
        private Vector2 _endPosition;

        public RectSelectionController(IInputProvider inputProvider,
            IUnitSelector unitSelector,
            GameplayCanvas gameplayCanvas)
        {
            _input = inputProvider;
            _unitSelector = unitSelector;
            _gameplayCanvas = gameplayCanvas;
        }

        public void Initialize()
        {
            _input.PlayerActions.LeftClick.performed += OnLeftClick;
            _input.PlayerActions.LeftClick.canceled += OnLeftClick;
        }

        public void Tick()
        {
            if (_isSelecting)
            {
                _endPosition = _input.ReadMousePosition();
                var rect = _gameplayCanvas.GetUIRectByScreenPoints(_startPosition, _endPosition);
                _unitSelector.OnSelecting(_endPosition);
                _gameplayCanvas.RectSelectionView.SetPositions(rect);
                _gameplayCanvas.RectSelectionView.SetVisible(true);
            }
            else
            {
                _gameplayCanvas.RectSelectionView.SetVisible(false);
            }
        }

        public void Dispose()
        {
            _input.PlayerActions.LeftClick.performed -= OnLeftClick;
            _input.PlayerActions.LeftClick.canceled -= OnLeftClick;
        }

        private void OnLeftClick(InputAction.CallbackContext obj)
        {
            _isSelecting = !_isSelecting;
            _unitSelector.OnStartSelect(_input.ReadMousePosition());
            _startPosition = _input.ReadMousePosition();
        }
    }
}