using UnityEngine;
using Milhouzer.Core.InventorySystem;
using Milhouzer.Core.InventorySystem.CraftingSystem;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class ProvideFuel : TaskBase
    {
        [SerializeField]
        protected new ProvideFuelTaskData _data;
        public new ProvideFuelTaskData Data => _data;

        protected IEnergyBasedCrafter _energyBasedCrafter;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {            
            _data = data as ProvideFuelTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(ProvideFuel)}: Invalid data type. Expected {nameof(ProvideFuelTaskData)}, but received {data?.GetType().Name ?? "null"}.");

             _energyBasedCrafter = target.GetComponent<IEnergyBasedCrafter>();
        
        }
        public override void Start()
        {
            if(_data.Inventory != null && _energyBasedCrafter != null)
            {
                taskRunState = TaskRunState.Failed;
            }

            taskRunState = TaskRunState.Running;
        }

        public override TaskRunState Execute()
        {
            IItemSlot slot = _data.Inventory.FindItemSlot(x => x.Stack.Item.Data.Category == _data.Category);
            if(slot == null)
            {
                Debug.Log("no fueld found on " + _data.Inventory);
                taskRunState = TaskRunState.Failed;
            }

            AddItemOperation operation = _energyBasedCrafter.ProvideInputFuel(slot.Stack);
            Debug.Log("Provided fuel " + operation.Added);
            switch(operation.Result)
            {
                case AddItemOperationResult.AddedAll:
                    _data.Inventory.RemoveItem(slot.Item);
                    taskRunState = TaskRunState.Finished;
                    break;
                case AddItemOperationResult.PartiallyAdded:
                    _data.Inventory.RemoveItem(slot.Item, operation.Added);
                    taskRunState = TaskRunState.Finished;
                    break;
                case AddItemOperationResult.AddedNone:
                    taskRunState = TaskRunState.Failed;
                    break;
            }

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
    public class ProvideFuelTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "ProvideFuel Task Data";
        public new string Name => _name;
        
        [HideInInspector]
        public IInventory Inventory;
        [SerializeField]
        public ItemCategory Category;

        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {
            Debug.Log("set data " + target);
            Inventory = target.GetComponent<IInventory>();
        }
    }
}
