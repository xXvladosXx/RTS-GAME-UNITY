using Codebase.Runtime.CameraSystem.Movement;
using Codebase.Runtime.CameraSystem.Rotation;
using Codebase.Runtime.CameraSystem.Zoom;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.InputSystem;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.CameraSystem.Factory
{
    public class CameraFactory : ICameraFactory, IInitializable
    {
        private readonly IInputProvider _inputProvider;
        private readonly GameLoopHandler _gameLoopHandler;
        public ICameraMain CameraMain { get; private set; }

        public CameraFactory(IInputProvider inputProvider,
            GameLoopHandler gameLoopHandler)
        {
            _inputProvider = inputProvider;
            _gameLoopHandler = gameLoopHandler;
        }

        public void Initialize()
        {
            CreateCameraMain();
        }

        public ICameraMain CreateCameraMain()
        {
            if(CameraMain != null)
                return CameraMain;

            var camera = Camera.main;

            var parent = camera.transform.parent.parent;
            var cameraMovements = new ICameraMovement[]
            {
                new CameraMovementFollowCharacter(),
                new CameraMovementBorders(_inputProvider, parent)
            };

            var cameraRotation = new CameraRotation(_inputProvider, parent);
            var cameraZoom = new CameraZoom(_inputProvider, parent, camera.transform.parent);
            
            CameraMain = new CameraMain(cameraMovements, cameraRotation, cameraZoom, camera);
            
            _gameLoopHandler.Add(CameraMain);
            
            return CameraMain;
        }
    }
}