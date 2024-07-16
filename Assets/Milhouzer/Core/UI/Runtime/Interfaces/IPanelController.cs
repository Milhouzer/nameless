using Milhouzer.Common.Interfaces;

namespace Milhouzer.Core.UI
{
    public interface IPanelController
    {
        public string ID { get; }
        public bool IsVisible { get; }
        public void Initialize(IUIDataSerializer data);
        public void Show();
        public void Hide();
        public void Destroy();
        public bool DestroyOnHide { get; }

        public event System.Action BeforeShow;
        public event System.Action AfterShow;
        public event System.Action BeforeHide;
        public event System.Action AfterHide;
    }
}
