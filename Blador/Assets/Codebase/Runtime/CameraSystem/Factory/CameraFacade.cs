using Codebase.Runtime.CameraSystem.Movement;
using Codebase.Runtime.CameraSystem.Rotation;
using Codebase.Runtime.CameraSystem.Zoom;
using Codebase.Runtime.GameplayCore;
using Codebase.Runtime.InputSystem;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.CameraSystem.Factory
{
    public class CameraFacade : ICameraFacade, IInitializable
    {
        private readonly IInputProvider _inputProvider;
        private readonly IRaycastHandler _raycastHandler;
        private readonly GameLoopHandler _gameLoopHandler;
        private ICameraMain _cameraMain;
        public ICameraMain CameraMain
        {
            get
            {
                if(_cameraMain == null)
                    Initialize();

                return _cameraMain;
            }
        }

        public CameraFacade(IInputProvider inputProvider,
            IRaycastHandler raycastHandler,
            GameLoopHandler gameLoopHandler)
        {
            _inputProvider = inputProvider;
            _raycastHandler = raycastHandler;
            _gameLoopHandler = gameLoopHandler;
        }

        public void Initialize()
        {
            CreateCameraMain();
        }

        public RaycastHit FireRay(Vector3 position, LayerMask layerMask) => 
            _raycastHandler.FireRay(position, layerMask, _cameraMain.Camera);

        public RaycastHit FireRay(Vector3 mousePosition) => 
            _raycastHandler.FireRay(mousePosition, _cameraMain.Camera);

        public void CreateCameraMain()
        {
            if(_cameraMain != null)
                return;

            var camera = Camera.main;

            var parent = camera.transform.parent.parent;
            var cameraMovements = new ICameraMovement[]
            {
                new CameraMovementFollowCharacter(),
                new CameraMovementBorders(_inputProvider, parent)
            };

            var cameraRotation = new CameraRotation(_inputProvider, parent);
            var cameraZoom = new CameraZoom(_inputProvider, parent, camera.transform.parent);
            
            _cameraMain = new CameraMain(cameraMovements, cameraRotation, cameraZoom, camera);
            
            _gameLoopHandler.Add(CameraMain);
        }
    }
}