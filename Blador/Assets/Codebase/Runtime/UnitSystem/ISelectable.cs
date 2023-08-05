namespace Codebase.Runtime.UnitSystem
{
    public interface ISelectable
    {
        void SetSelected(bool selected);
        bool IsSelected { get; }
    }
}