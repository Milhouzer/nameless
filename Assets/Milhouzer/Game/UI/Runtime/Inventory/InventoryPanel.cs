using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Milhouzer.Common.Interfaces;
using Milhouzer.Core.UI;
using Milhouzer.Core.InventorySystem;
using UnityEngine;

namespace Milhouzer.Game.UI
{
    public class InventoryPanel : UIPanel<IInventory>
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
    
        [SerializeField]
        protected string _inventoryName;
        protected IInventory _inventory;

        protected override void Awake()
        {
            if(string.IsNullOrEmpty(_id))
            {
                _id = UIManager.Settings.INVENTORY_PANEL_ID;
            }

            if(!dynamic)
            {
                slots = slotsContainer.GetComponentsInChildren<ItemSlotUI>().ToList();
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
            if(_inventory != null)
            {
                _inventory.OnItemAdded -= Inventory_ItemAdded;
                _inventory.OnItemRemoved -= Inventory_ItemRemoved;
            }
            
            base.Hide();
        }

        protected override void SetVisibility(bool value)
        {
            base.SetVisibility(value);
        }

        protected override void OnInitialize(IUIDataSerializer data)
        {
            if(_inventory != null)
            {
                _inventory.OnItemAdded -= Inventory_ItemAdded;
                _inventory.OnItemRemoved -= Inventory_ItemRemoved;
            }

            _inventory = (IInventory)data.SerializeUIData()["Inventory"];

            _inventory.OnItemAdded += Inventory_ItemAdded;
            _inventory.OnItemRemoved += Inventory_ItemRemoved;
            
            if(!dynamic)
            {
                slots = slotsContainer.GetComponentsInChildren<ItemSlotUI>().ToList();
            }
            
        }

        private void Inventory_ItemRemoved(RemoveItemOperation operation)
        {
            Refresh();
        }

        private void Inventory_ItemAdded(AddItemOperation operation)
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
            

            foreach(IItemSlot itemSlot in _inventory.Slots)
            {
                if(itemSlot.Item.Data == null)
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

                ReadOnlyCollection<IItemSlot> inventorySlots = _inventory.GetInventory(_inventoryName);
                for (int i = 0; i < inventorySlots.Count; i++)
                {
                    ItemSlotUI slot = Instantiate(slotPrefab);
                    slot.gameObject.transform.SetParent(slotsContainer);
                    slots.Add(slot);
                }

                foreach(IItemSlot itemSlot in inventorySlots)
                {
                    if(itemSlot == null)
                        continue;
                    
                    if(itemSlot.Item.Data == null)
                        continue;

                    ItemSlotUI slotUI = slots[itemSlot.Index];
                    slotUI.SetItem(itemSlot);
                }
            }
            else
            {
                Debug.Log("Refresh inventory " + _inventory);
                ReadOnlyCollection<IItemSlot> inventorySlots = _inventory.GetInventory(_inventoryName);
                for(int j = 0; j < slots.Count; j++)
                {
                    slots[j].SetItem(j < inventorySlots.Count ? inventorySlots[j] : null);
                }
            }
        }
    }
}
