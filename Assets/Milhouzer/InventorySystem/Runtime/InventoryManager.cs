using System;
using System.Collections.Generic;
using System.Linq;
using Milhouzer.Common.Utility;
using Milhouzer.InventorySystem.CraftingSystem;
using UnityEngine;
using static Milhouzer.InventorySystem.CraftDatabase;

namespace Milhouzer.InventorySystem
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [SerializeField]
        private GameObject DROPPED_ITEM_BASE;
        public ItemDatabase Items;
        public CraftDatabase Crafts;

        public static GameObject DisplayItem(IItemStack item, Vector3 pos)
        {
            if(item == null)
            {
                return null;
            }

            GameObject displayedItem = Instantiate(item.Item.Data.RenderModel);
            displayedItem.transform.position = pos;

            return displayedItem;
        }
        
        public static GameObject DisplayItem(IItemStack item, Vector3 pos, Transform parent)
        {
            if(item == null)
            {
                return null;
            }
            
            GameObject displayedItem = Instantiate(item.Item.Data.RenderModel, parent);
            displayedItem.transform.position = pos;

            return displayedItem;
        }

        public static GameObject DropItem(IItemStack item, Transform parent)
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

        public static GameObject DropItem(IItemStack item, Vector3 pos)
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

        public static GameObject DropItem(IItemStack item, Vector3 pos, Quaternion rot)
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


        public static GameObject DropItem(IItemStack item, Vector3 pos, Quaternion rot, Transform parent)
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


        public bool CanCraftItem(InventoryBase inventory, string item, CraftingProcess process)
        {
            if(!Crafts.CookRecipes.ContainsKey(item))
                return false;

            List<Ingredient> ingredients = new();
            foreach(ISlot slot in inventory.Slots)
            {
                ingredients.Add(new Ingredient(slot.Stack.Item.Data.ID, slot.Stack.Amount));
            }

            List<Ingredient> needed = Crafts.CookRecipes[item];
            foreach (Ingredient ingredient in ingredients)
            {
                if(!needed.Any(x => x.name == ingredient.name && x.amount <= ingredient.amount) )
                    return false;
            }

            return true;
        }
        
        public bool CraftItem(InventoryBase inventory, InventoryBase output, string item, CraftingProcess process)
        {
            if(!CanCraftItem(inventory, item, process))
                return false;

            List<Ingredient> needed = Crafts.CookRecipes[item];

            IItem crafted =  CreateItem(item);
            AddItemOperation op = output.AddItem(crafted);

            if(op.Result == AddItemOperationResult.AddedNone)
                return false;

            foreach(Ingredient ingredient in needed)
            {
                IItem ingredientItem = inventory.FindItem(x => x.Data.ID == ingredient.name);

                RemoveItemOperation operation = inventory.RemoveItem(ingredientItem);
            }

            return true;
        }

        private IItem CreateItem(string ID)
        {
            BaseItem item = new BaseItem(Instance.Items.FindEntry(x => x.ID == ID));

            return item;
        }
    }
}
