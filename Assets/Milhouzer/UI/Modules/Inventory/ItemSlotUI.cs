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

        protected ItemSlot itemSlot;

        public void SetItem(ItemSlot item)
        {
            itemSlot = item;
            Refresh();
            OnSetItem(item);
        }

        protected virtual void OnSetItem(ItemSlot item)
        {

        }


        protected virtual void Refresh()
        {
            if(itemSlot == null || itemSlot.Stack == null || itemSlot.Data == null)
            {
                Reset();
                return;
            }

            Stack.text = itemSlot.Stack.Amount.ToString();
            Icon.sprite = itemSlot.Data.Sprite;
        }

        protected virtual void Reset()
        {
            Stack.text = "";
            Icon.SetAlpha(0f);
        }
    }
}
