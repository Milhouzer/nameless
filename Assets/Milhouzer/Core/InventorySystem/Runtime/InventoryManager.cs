using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Milhouzer.Common.Utility;
using Milhouzer.Core.InventorySystem.CraftingSystem;
using Milhouzer.Core.InventorySystem.ItemProcessing;
using UnityEngine;
using static Milhouzer.Core.InventorySystem.CraftDatabase;

namespace Milhouzer.Core.InventorySystem
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [SerializeField]
        private GameObject DROPPED_ITEM_BASE;
        public ItemDatabase Items;
        public CraftDatabase Crafts;

        protected override void Awake()
        {
            base.Awake();
            Crafts.GetReversedRecipes();
        }
        
        public static GameObject RenderItemModel(IItem item, Vector3 pos, Transform parent)
        {
            if(item == null)
            {
                return null;
            }
            
            GameObject displayedItem = Instantiate(item.Data.RenderModel, parent);
            displayedItem.transform.position = pos;

            return displayedItem;
        }

        internal static GameObject DropItem(IItemStack item, Transform parent)
        {
            if(item == null)
            {
                return null;
            }

            GameObject droppedItemGO = Instantiate(InventoryManager.Instance.DROPPED_ITEM_BASE, parent);
            IItemHolder itemHolder = droppedItemGO.GetComponent<IItemHolder>();
            
            itemHolder.Hold(item);

            return droppedItemGO;
        }

        internal static GameObject DropItem(IItemStack item, Vector3 pos)
        {
            if(item == null)
            {
                return null;
            }

            GameObject droppedItemGO = Instantiate(InventoryManager.Instance.DROPPED_ITEM_BASE);
            IItemHolder itemHolder = droppedItemGO.GetComponent<IItemHolder>();
            
            itemHolder.Hold(item);

            droppedItemGO.transform.position = pos;

            return droppedItemGO;
        }

        internal static GameObject DropItem(IItemStack item, Vector3 pos, Quaternion rot)
        {
            if(item == null)
            {
                return null;
            }

            GameObject droppedItemGO = Instantiate(InventoryManager.Instance.DROPPED_ITEM_BASE, pos, rot);
            IItemHolder itemHolder = droppedItemGO.GetComponent<IItemHolder>();
            
            itemHolder.Hold(item);

            return droppedItemGO;
        }


        internal static GameObject DropItem(IItemStack item, Vector3 pos, Quaternion rot, Transform parent)
        {
            if(item == null)
            {
                return null;
            }

            GameObject droppedItemGO = Instantiate(InventoryManager.Instance.DROPPED_ITEM_BASE, pos, rot, parent);
            IItemHolder itemHolder = droppedItemGO.GetComponent<IItemHolder>();
            
            itemHolder.Hold(item);

            return droppedItemGO;
        }

        public List<string> GetCraftResults(ReadOnlyCollection<IItemSlot> slots, ProcessType process)
        {
            // TODO : use process here
            return Crafts.FindRecipes(slots, 0);
        }

        internal bool CanCraftItem(IInventory inventory, string item, CraftingProcess process)
        {
            if(!Crafts.CookRecipes.ContainsKey(item))
                return false;

            List<Ingredient> ingredients = new();
            foreach(IItemSlot slot in inventory.Slots)
            {
                ingredients.Add(new Ingredient(slot.Item.Data.ID, slot.Stack.Amount));
            }

            List<Ingredient> needed = Crafts.CookRecipes[item];
            foreach (Ingredient ingredient in ingredients)
            {
                if(!needed.Any(x => x.name == ingredient.name && x.amount <= ingredient.amount) )
                    return false;
            }

            return true;
        }
        
        internal bool CraftItem(IInventory inventory, IInventory output, string item, CraftingProcess process)
        {
            if(!CanCraftItem(inventory, item, process))
                return false;

            List<Ingredient> needed = Crafts.CookRecipes[item];

            IItemData itemData = GetItemData(item);
            if(itemData == null)
            {
                Debug.LogError($"ItemData for {item} does not exist in database");
                return false;
            }

            IItem crafted = new BaseItem(itemData); 
            AddItemOperation op = output.AddItem(new ItemStack(crafted.Data, 1));

            if(op.Result == AddItemOperationResult.AddedNone)
                return false;

            foreach(Ingredient ingredient in needed)
            {
                IItem ingredientItem = inventory.FindItem(x => x.Stack.Item.Data.ID == ingredient.name);

                RemoveItemOperation operation = inventory.RemoveItem(ingredientItem);
            }

            return true;
        }

        public IItemData GetItemData(string id)
        {
            return Items.FindEntry(x => x.ID == id);
        }
    }
}
