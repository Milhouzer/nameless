using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Milhouzer.Common.Interfaces;
using Milhouzer.InventorySystem;
using UnityEngine;

namespace Milhouzer.UI.InventorySystem
{
    public class SegmentedInventoryPanel : UIPanel<IInventory>
    {
        [SerializeField]
        private List<InventoryPanel> panels;

        protected override void Awake()
        {
            if(string.IsNullOrEmpty(_id))
            {
                _id = UIManager.Settings.COOKER_ID;
            }

            base.Awake();
        }

        public override void Show()
        {
            foreach(InventoryPanel panel in panels) {
                panel.Show();
            }
            base.Show();
        }

        public override void Hide()
        {
            foreach(InventoryPanel panel in panels) {
                panel.Hide();
            }

            base.Hide();
        }

        protected override void SetVisibility(bool value)
        {
            base.SetVisibility(value);
        }

        protected override void OnInitialize(IUIDataSerializer data)
        {
            foreach(InventoryPanel panel in panels) {
                panel.Initialize(data);
            }
        }
    }
}
