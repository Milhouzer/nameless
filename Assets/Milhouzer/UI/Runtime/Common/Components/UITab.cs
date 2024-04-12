using Milhouzer.Common.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Milhouzer.UI
{
    public class UITab : MonoBehaviour, ISelectableUI, IPointerUpHandler
    {
        bool isSelected = false;
        public bool IsSelected => isSelected;
        
        // [SerializeField]
        // private UIPanel panel;

        private TabGroup tabGroup;
        public TabGroup Group
        {
            get => tabGroup;
            set => tabGroup = value;
        }

        public bool Select()
        {
            isSelected = true;
            ISelectableUI.Current = this;
            // panel.Show();
            return true;
        }


        public bool UnSelect()
        {
            isSelected = false;
            if(ISelectableUI.Current == (ISelectableUI)this)
            {
                ISelectableUI.Current = null;
            }
            // panel.Hide();
            return true;
        }
    
        public void OnPointerUp(PointerEventData eventData)
        {
            if(!IsSelected)
            {
                tabGroup.SelectTab(this);
            }
        }
    }
}