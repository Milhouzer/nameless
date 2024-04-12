namespace Milhouzer.Common.Interfaces
{
    public interface ISelectable 
    {
        bool IsSelected { get; }

        bool Select();
        bool UnSelect();

        public static ISelectable Current;
        
        public delegate void SelectEvent();
        public delegate void UnSelectEvent();

        public event SelectEvent OnSelected;
        public event UnSelectEvent OnUnSelected;
    }
}