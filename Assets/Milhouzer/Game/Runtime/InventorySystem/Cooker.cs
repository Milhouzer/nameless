using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Milhouzer.Common.Interfaces;
using Milhouzer.InventorySystem.ItemProcessing;
using Milhouzer.InventorySystem.Restrictions;
using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public class Cooker : ProcessorBase, IInspectable
    {
        public Transform WorldTransform => throw new System.NotImplementedException();

        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Panel", GetType().Name},
                {"Inventory", inventory},                 
            };
        }

        [SerializeField]
        SegmentedInventory inventory;

        protected void Awake() {
            inventory.OnItemAdded += Inventory_OnItemAdded;
            inventory.OnItemRemoved += Inventory_OnItemRemoved;
        }

        private void Inventory_OnItemAdded(AddItemOperation op)
        {
            Debug.Log("Item added");
            StartProcess();
        }

        private void Inventory_OnItemRemoved(RemoveItemOperation op)
        {
            StartProcess();
        }

        /// <TODO>
        ///  Hook this method to ivnentory add item.
        /// Create IProcessorEvents interface ?? (out of context but split IInventory interface into multiple interfaces)
        /// </TODO>
        private bool StartProcess(IProcessable processed)
        {
            if(!CanProcess(processed))
                return false;
            

            return true;
        }

        private IItem CalculateOutput()
        {
            return null;
        }

        public override bool StartProcess()
        {
            ReadOnlyCollection<IItemSlot> input = inventory.GetInventory("input");
            if(!input.All(slot => slot.Item != null))
            {
                Debug.Log("input count is 0");
                return false;
            }

            ReadOnlyCollection<IItemSlot> fuel = inventory.GetInventory("fuel");
            if(!fuel.All(slot => slot.Item != null))
            {
                Debug.Log("fuel count is 0");
                return false;
            }

            ReadOnlyCollection<IItemSlot> output = inventory.GetInventory("output");
            // if(output.Count == 0){
                // Immediatly start process with input items
                // TODO: decide what to do when there are multiple possible recipes. Should this be possible ??
                List<string> results = InventoryManager.Instance.GetCraftResults(input, Process);
                if(results.Count == 0)
                {
                    Debug.Log("No possible output");
                    return false;
                }

                _pendingProcess = new Process(
                    _process,
                    5f,
                    () =>{
                        Debug.Log("FINISHED PROCESS");
                        IItemData data = InventoryManager.Instance.GetItemData(results[0]);
                        AddItemOperation op = inventory.AddItem(new ItemStack(data, 1), "output");
                        
                        if(op.Result == AddItemOperationResult.AddedAll)
                        {
                            inventory.RemoveItem(0, 1, "input");
                        }
                    }
                );

                foreach (string r in results)
                {
                    Debug.Log("Can cook : " + r);
                }
            // } else {
            //     // Check if process output is compatible (here stackable because the output will produce an item) with existing output and do it if so
            //     IItem item = CalculateOutput();
            //     Debug.Log("output count is 0");
            //     if(!inventory.CanAddItem(item))
            //         return false;

            //     return true;
            // }
            
            return true;
        }

        public override void FinishProcess()
        {
            _pendingProcess.OnFinish.Invoke();
            // inventory.RemoveItem()
            _pendingProcess = default;
            // IItemData data = InventoryManager.Instance.GetItemData("applesauce");

            // ItemStack itemStack = new ItemStack(data, 1);

            // StartProcess();
        }
    }
}