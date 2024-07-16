using UnityEngine;
using Milhouzer.Core.InventorySystem;
using Milhouzer.Core.InventorySystem.ItemProcessing;
using System.Collections.Generic;
using Milhouzer.Core.InventorySystem.Restrictions;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class AddInputItem : TaskBase
    {
        [SerializeField]
        protected new AddInputItemTaskData _data;
        public new AddInputItemTaskData Data => _data;

        protected ItemStack _itemStack;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as AddInputItemTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(AddInputItem)}: Invalid data type. Expected {nameof(AddInputItemTaskData)}, but received {data?.GetType().Name ?? "null"}.");

             _itemStack = new ItemStack(_data.Item.Data, _data.Item.Amount);
        
        }
        public override void Start()
        {
            if(_data.Inventory != null)
            {
                AddItemOperation operation = _data.Inventory.AddItem(_itemStack);
                if(operation.Result != AddItemOperationResult.AddedAll)
                {

                }
                taskRunState = TaskRunState.Finished;
            }
            else
            {
                taskRunState = TaskRunState.Failed;
            }
        }

        public override TaskRunState Execute()
        {
            return taskRunState;
        }

        public override bool Complete()
        {
            return true;
        }

        public override void Stop()
        {

        }
        
        public override ITaskData GetData()
        {
            return _data;
        }
    }

    [System.Serializable]
    public class AddInputItemTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "AddInputItem";
        /// <summary>
        /// Name of the task
        /// </summary>
        public new string Name => _name;
        
        /// <summary>
        /// Inventory to add the item to.
        /// </summary>
        [HideInInspector]
        public IInventory Inventory;

        /// <summary>
        /// Item to add
        /// </summary>
        public ItemStackDefinition Item;

        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {
            Inventory = target.GetComponent<IInventory>();
        }
    }
}
