using Codebase.Runtime.InputSystem;
using UnityEngine;

namespace Codebase.Runtime.CameraSystem.Zoom
{
    class CameraZoom : ICameraZoom
    {
        private readonly IInputProvider _inputProvider;
        private readonly Transform _transform;
        private readonly Transform _cameraHolder;

        private readonly Vector2 _bounds = new Vector2(10f, 70f);
        private Vector3 _targetPosition;
        private float _input;

        private const float SMOOTHING = 5f;

        private Vector3 CameraDirection => _transform.InverseTransformDirection(_cameraHolder.forward);

        public CameraZoom(IInputProvider inputProvider,
            Transform transform,
            Transform cameraHolder)
        {
            _inputProvider = inputProvider;
            _transform = transform;
            _cameraHolder = cameraHolder;
            
            _targetPosition = _cameraHolder.localPosition;
        }
        
        public void Zoom(Transform transform, float speed)
        {
            _input = _inputProvider.ScrollAxis;
            Vector3 nextTargetPosition = _targetPosition + CameraDirection * (_input * speed);
            
            if (IsInBounds(nextTargetPosition)) 
                _targetPosition = nextTargetPosition;
            
            _cameraHolder.localPosition =
                Vector3.Lerp(_cameraHolder.localPosition, _targetPosition, Time.deltaTime * SMOOTHING);
        }
        
        private bool IsInBounds(Vector3 position) => 
            position.magnitude > _bounds.x && position.magnitude < _bounds.y;
    }
}