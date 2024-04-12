using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Milhouzer.UI
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField]
        List<UITab> tabs = new();

        private UITab currentSelected;
        public UITab CurrentSelected => currentSelected;

        protected virtual void Awake()
        {
            RegisterTabs();
        }

        protected void RegisterTabs()
        {
            if(tabs.Count > 0)
            {
                foreach(UITab tab in tabs)
                {
                    tab.Group = this;
                }

                SelectTab(tabs[0]);
            }
        }

        public void SelectTab(UITab tab)
        {
            if(currentSelected != null)
            {
                currentSelected.UnSelect();
            }

            currentSelected = tab;
            tab.Select();
        }
    }
}
