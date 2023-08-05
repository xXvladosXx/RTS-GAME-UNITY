namespace Codebase.Runtime.CameraSystem.Factory
{
    public interface ICameraFactory
    {
        ICameraMain CameraMain { get; }
        ICameraMain CreateCameraMain();
    }
}