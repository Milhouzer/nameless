using Milhouzer.InventorySystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Milhouzer.Common.Utility;

namespace Milhouzer.UI.InventorySystem
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField]
        protected Image Icon;
        [SerializeField]
        protected TextMeshProUGUI Stack;

        protected IItemSlot itemSlot;

        public void SetItem(IItemSlot item)
        {
            itemSlot = item;
            Refresh();
            OnSetItem(item);
        }

        protected virtual void OnSetItem(IItemSlot item)
        {

        }


        protected virtual void Refresh()
        {
            if(itemSlot == null || itemSlot.IsEmpty())
            {
                Reset();
                return;
            }

            Stack.text = itemSlot.Stack.Amount.ToString();
            Icon.sprite = itemSlot.Item.Data.Sprite;
            Icon.SetAlpha(1f);
        }

        protected virtual void Reset()
        {
            Stack.text = "";
            Icon.SetAlpha(0f);
        }
    }
}
