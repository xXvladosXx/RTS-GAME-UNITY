using Codebase.Runtime.InputSystem;
using UnityEngine;

namespace Codebase.Runtime.CameraSystem.Rotation
{
    class CameraRotation : ICameraRotation
    {
        private readonly IInputProvider _inputProvider;
        
        private float _targetAngle;
        private float _currentAngle;

        private const float SMOOTHING = 5f;

        public CameraRotation(IInputProvider inputProvider,
            Transform transform)
        {
            _inputProvider = inputProvider;
            
            _targetAngle = transform.eulerAngles.y;
            _currentAngle = _targetAngle;
        }
        
        public void Rotate(Transform transform, float speed)
        {
            if(!_inputProvider.IsRightButtonUp())
                return;
            
            _targetAngle += _inputProvider.MouseAxis * speed;
            _currentAngle = Mathf.LerpAngle(_currentAngle, _targetAngle, SMOOTHING * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.up);
        }
    }
}