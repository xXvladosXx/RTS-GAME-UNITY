using Codebase.Runtime.CameraSystem.Movement;
using Codebase.Runtime.CameraSystem.Rotation;
using Codebase.Runtime.CameraSystem.Zoom;
using Codebase.Runtime.GameplayCore;
using UnityEngine;

namespace Codebase.Runtime.CameraSystem
{
    class CameraMain : ICameraMain
    {
        private readonly ICameraMovement[] _cameraMovements;
        private readonly ICameraRotation _cameraRotation;
        private readonly ICameraZoom _cameraZoom;

        private ICameraMovement _currentMovement;
        private Transform _cameraRoot;
        public Camera Camera { get; }
        
        public const float DEFAULT_CAMERA_SPEED = .01f;
        public const float DEFAULT_ROTATION_CAMERA_SPEED = 5f;

        public CameraMain(ICameraMovement[] cameraMovements,
            ICameraRotation cameraRotation,
            ICameraZoom cameraZoom,
            Camera camera)
        {
            _cameraMovements = cameraMovements;
            _cameraRotation = cameraRotation;
            _cameraZoom = cameraZoom;

            _currentMovement = _cameraMovements[1];
            Camera = camera;
            _cameraRoot = Camera.transform.parent.parent;
        }
        
        public void SwitchMovement()
        {
            _currentMovement = _cameraMovements[1];
        }
        
        public bool GameUpdate()
        {
            Move();
            return true;            
        }

        private void Move()
        {
            _currentMovement.Move(_cameraRoot, DEFAULT_CAMERA_SPEED);
            _cameraRotation.Rotate(_cameraRoot, DEFAULT_ROTATION_CAMERA_SPEED);
            _cameraZoom.Zoom(_cameraRoot, DEFAULT_ROTATION_CAMERA_SPEED);
        }

        public void Recycle()
        {
        }
    }
}