using System.Collections.Generic;
using System.Linq;
using Milhouzer.InventorySystem;
using UnityEngine;

namespace Milhouzer.UI.InventorySystem
{
    public class InventoryPanel : UIPanel<InventoryBase>
    {
        [SerializeField]
        protected Transform slotsContainer;
        [SerializeField]
        private ItemSlotUI slotPrefab;

        /// <summary>
        /// If true, destroy and instantiate new slots each time based on the count of the inventory
        /// otherwise instantiate number of slots when initialized.
        /// </summary>
        [SerializeField]
        private bool dynamic;
        protected List<ItemSlotUI> slots = new();
    
        protected InventoryBase _inventory;

        protected override void Awake()
        {
            if(string.IsNullOrEmpty(_id))
            {
                _id = UIManager.Settings.INVENTORY_PANEL_ID;
            }

            base.Awake();
        }

        public override void Show()
        {
            Refresh();
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        protected override void SetVisibility(bool value)
        {
            base.SetVisibility(value);
        }

        protected override void OnInitialize(InventoryBase inventory)
        {
            if(_inventory != null)
            {
                _inventory.OnItemAdded -= Inventory_ItemAdded;
                _inventory.OnItemRemoved -= Inventory_ItemRemoved;
            }

            _inventory = inventory;

            _inventory.OnItemAdded += Inventory_ItemAdded;
            _inventory.OnItemRemoved += Inventory_ItemRemoved;
            
            if(!dynamic)
            {
                slots = slotsContainer.GetComponentsInChildren<ItemSlotUI>().ToList();
            }
            
        }

        private void Inventory_ItemRemoved(ItemOperationEventData eventData)
        {
            Refresh();
        }

        private void Inventory_ItemAdded(ItemOperationEventData eventData)
        {
            Refresh();
        }

        protected void RefreshSlots()
        {
            if(_inventory == null)
                return;
                

            for (int i = 0; i < slots.Count; i++)
            {
                ItemSlotUI slotUI = slots[i];
                slotUI.SetItem(null);
            }
            

            foreach(ItemSlot itemSlot in _inventory.Slots)
            {
                if(itemSlot.Data == null)
                    continue;

                ItemSlotUI slotUI = slots[itemSlot.Index - 1];
                slotUI.SetItem(itemSlot);
            }
        }


        /// <summary>
        /// Refresh inventory panel : display every slots.
        /// Slots are instantiated if the container is dynamic. They are only refresh with the corresponding item
        /// in inventory.
        /// </summary>
        public override void Refresh()
        {
            if(_inventory == null)
                return;


            if(dynamic)
            {
                for (int j = slotsContainer.childCount - 1; j >= 0; j--)
                {
                    Destroy(slotsContainer.GetChild(j).gameObject);
                }

                slots = new List<ItemSlotUI>();

                for (int i = 0; i < _inventory.MaxSlots; i++)
                {
                    ItemSlotUI slot = Instantiate(slotPrefab);
                    slot.gameObject.transform.SetParent(slotsContainer);
                    slots.Add(slot);
                }

                foreach(ItemSlot itemSlot in _inventory.Slots)
                {
                    if(itemSlot == null)
                        continue;
                    
                    if(itemSlot.Data == null)
                        continue;

                    ItemSlotUI slotUI = slots[itemSlot.Index];
                    slotUI.SetItem(itemSlot);
                }
            }
            else
            {
                for(int j = 0; j < slots.Count; j++)
                {
                    if(j < _inventory.Slots.Count)
                    {
                        ItemSlot slot = _inventory.Slots[j];
                            
                        ItemSlotUI slotUI = slots[slot.Index];
                        slotUI.SetItem(slot);
                    }else
                    {
                        ItemSlotUI slotUI = slots[j];
                        slotUI.SetItem(null);
                    }

                }
            }
        }
    }
}
