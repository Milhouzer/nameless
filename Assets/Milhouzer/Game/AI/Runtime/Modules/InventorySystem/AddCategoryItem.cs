using UnityEngine;
using Milhouzer.Core.InventorySystem;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class AddCategoryItem : TaskBase
    {
        [SerializeField]
        protected new AddCategoryItemTaskData _data;
        public new AddCategoryItemTaskData Data => _data;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as AddCategoryItemTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(AddCategoryItem)}: Invalid data type. Expected {nameof(AddCategoryItemTaskData)}, but received {data?.GetType().Name ?? "null"}.");
        
        }
        public override void Start()
        {
            if(_data.Inventory != null)
            {
                IItemSlot slot = _data.Inventory.FindItemSlot(x => x.Item.Data.Category == _data.Category);
                AddItemOperation operation = _data.Inventory.AddItem(new ItemStack(slot.Stack.Item.Data, slot.Stack.Amount), _data.InventoryName);
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
    public class AddCategoryItemTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "AddItem";
        /// <summary>
        /// Name of the task
        /// </summary>
        public new string Name => _name;
        

        [SerializeField]
        private string _inventoryName = "";
        /// <summary>
        /// Name of the inventory to add the item to
        /// </summary>
        public string InventoryName => _inventoryName;

        /// <summary>
        /// Inventory to add the item to.
        /// </summary>
        private IInventory _inventory;
        public IInventory Inventory => _inventory;

        /// <summary>
        /// Item to add
        /// </summary>
        public ItemCategory Category;

        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {
            _inventory = target.GetComponent<IInventory>();
        }
    }
}
