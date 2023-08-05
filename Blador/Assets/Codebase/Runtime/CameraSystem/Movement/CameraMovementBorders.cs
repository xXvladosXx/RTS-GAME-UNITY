using Codebase.Runtime.InputSystem;
using UnityEngine;

namespace Codebase.Runtime.CameraSystem.Movement
{
    class CameraMovementBorders : ICameraMovement
    {
        private readonly IInputProvider _inputProvider;
        private readonly Camera _cameraMain;

        private Vector3 _targetPosition;
        private Vector3 _trajectory;
        
        private const float SMOOTHING = 5f;

        public CameraMovementBorders(IInputProvider inputProvider,
            Transform camera)
        {
            _inputProvider = inputProvider;
            
            _targetPosition = camera.position;
        }
        
        public void Move(Transform transform, float speed)
        {
            float x = _inputProvider.Axis.x;
            float z = _inputProvider.Axis.y;
            
            Vector3 right = transform.right * x;
            Vector3 forward = transform.forward * z;
            
            _trajectory = (right + forward).normalized;
            
            Vector3 nextTargetPosition = _targetPosition + _trajectory * speed;
            _targetPosition = nextTargetPosition;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, SMOOTHING * Time.deltaTime);
        }
    }
}