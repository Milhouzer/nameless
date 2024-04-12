namespace Milhouzer.Common.Interfaces
{
    public interface ISelectableUI 
    {
        bool IsSelected { get; }

        bool Select();
        bool UnSelect();

        public static ISelectableUI Current;
    }
}