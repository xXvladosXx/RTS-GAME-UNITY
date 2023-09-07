using Codebase.Runtime.CameraSystem;
using Codebase.Runtime.CameraSystem.Factory;
using Codebase.Runtime.Selection;
using UnityEngine;
using Zenject;

namespace Codebase.Runtime.UI
{
    public class GameplayCanvas : MonoBehaviour
    {
        [field: SerializeField] public RectTransform SelectionBox { get; private set; }
        [field: SerializeField] public RectSelectionView RectSelectionView { get; private set; }
        [SerializeField] private RectTransform _canvasRectTransform;
        private ICameraMain _camera;

        [Inject]
        public void Construct(ICameraFacade cameraFacade)
        {
            _camera = cameraFacade.CameraMain;
        }

        public Rect GetUIRectByScreenPoints(Vector2 inputStartPosition, Vector2 inputEndPosition)
        {
            inputStartPosition = GetUIPointBeScreenPoint(inputStartPosition);
            inputEndPosition = GetUIPointBeScreenPoint(inputEndPosition);

            var center = (inputStartPosition + inputEndPosition) / 2;

            var vector = inputEndPosition - inputStartPosition;
            var size = new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

            var rect = new Rect(center, size);
            return rect;
        }

        public Vector2 GetUIPointBeScreenPoint(Vector2 screenPoint)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRectTransform, screenPoint, _camera.Camera, out var localPoint);

            return localPoint;
        }
    }
}