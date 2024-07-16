using UnityEngine;
using Milhouzer.Core.InventorySystem;
using Milhouzer.Core.InventorySystem.Restrictions;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class AddItem : TaskBase
    {
        [SerializeField]
        protected new AddItemTaskData _data;
        protected AddItemOperation result;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as AddItemTaskData;
            if(_data == null) throw new System.InvalidOperationException($"Initialization failed for {nameof(AddItem)}: Invalid data type. Expected {nameof(AddItemTaskData)}, but received {data?.GetType().Name ?? "null"}.");

        
        }
        public override void Start()
        {
            if(_data.Instigator == null || _data.Target == null)
            {
                taskRunState = TaskRunState.Failed;
                return;
            }

            IItemSlot slot = _data.Instigator.FindItemSlot(_data.Restrictions);
            Debug.Log($"[AddItem] found slot: {slot} containing {slot.Item.Data.DisplayName}");
            if(slot == null)
            {
                taskRunState = TaskRunState.Failed;
                return;
            }

            result = _data.Target.AddItem(slot.Stack, _data.InventoryName);
            Debug.Log($"add item: try add {slot.Item.Data.DisplayName}: {result.Result}");

            
            // TODO : Make this mappable in data (same in RemoveItem task)
            // WHY ?
            switch(result.Result)
            {
                case AddItemOperationResult.AddedAll:
                    RemoveItemOperation op = _data.Instigator.RemoveItem(slot.Index);
                    Debug.Log($"{op.Result} from {_data.Instigator}");
                    taskRunState = TaskRunState.Finished;
                    break;
                case AddItemOperationResult.PartiallyAdded:
                    _data.Instigator.RemoveItem(slot.Index, result.Added);
                    taskRunState = TaskRunState.Finished;
                    break;
                case AddItemOperationResult.AddedNone:
                    taskRunState = TaskRunState.Failed;
                    break;
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
    public class AddItemTaskData : BaseTaskData
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
