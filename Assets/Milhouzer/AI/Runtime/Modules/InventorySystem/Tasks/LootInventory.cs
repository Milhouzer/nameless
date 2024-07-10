using UnityEngine;
using Milhouzer.InventorySystem;
using Milhouzer.InventorySystem.Restrictions;

namespace Milhouzer.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class LootInventory : TaskBase
    {
        [SerializeField]
        protected new LootInventoryTaskData _data;
        public new LootInventoryTaskData Data => _data;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as LootInventoryTaskData;
            if(_data == null) throw new System.InvalidOperationException($"Initialization failed for {nameof(LootInventory)}: Invalid data type. Expected {nameof(AddItemTaskData)}, but received {data?.GetType().Name ?? "null"}.");

        
        }
        public override void Start()
        {
            if(_data.Instigator == null || _data.Target == null)
            {
                taskRunState = TaskRunState.Failed;
                return;
            }
            int i = 0;
            foreach(IItemSlot slot in _data.Target.Slots)
            {
                RemoveItemOperation operation = _data.Target.RemoveItem(i);
                
                if(operation.Result == RemoveItemOperationResult.RemovedNone)
                    continue;
                    
                _data.Instigator.AddItem(new ItemStack(slot.Stack.Item.Data, operation.Removed), _data.InventoryName);

                i++;
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
    public class LootInventoryTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "Loot Inventory";
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
        [HideInInspector]
        public IInventory Target;
        /// <summary>
        /// Inventory to add the item to.
        /// </summary>
        [HideInInspector]
        public IInventory Instigator;

        [SerializeField]
        private InventoryRestrictions _restrictions;
        public InventoryRestrictions Restrictions => _restrictions;

        /// <summary>
        /// Get target and instigator inventories.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="instigator"></param>
        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {
            Target = target.GetComponent<IInventory>();
            Instigator = instigator.GetComponent<IInventory>();
        }
    }
}
