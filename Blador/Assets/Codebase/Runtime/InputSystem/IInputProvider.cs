using UnityEngine;

namespace Codebase.Runtime.InputSystem
{
    public interface IInputProvider
    {
        Vector2 Axis { get; }
        Vector2 ReadMousePosition();
        float ScrollAxis { get; }
        float MouseAxis { get; }
        PlayerInput.PlayerActions PlayerActions { get; }
        bool IsLeftButtonUp();
        bool IsRightButtonUp();
        void DisablePlayer();
        void EnablePlayer();
        bool IsMouseWheelPressed();
    }
}