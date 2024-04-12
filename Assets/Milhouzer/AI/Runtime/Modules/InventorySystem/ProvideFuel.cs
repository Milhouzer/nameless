using UnityEngine;
using Milhouzer.InventorySystem;
using Milhouzer.InventorySystem.CraftingSystem;

namespace Milhouzer.AI.Modules.InventorySystem
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
            ItemSlot slot = InventoryUtility.FindItemByCategory(_data.Inventory,  _data.Category);
            if(slot == null)
            {
                Debug.Log("no fueld found on " + _data.Inventory);
                taskRunState = TaskRunState.Failed;
            }

            AddItemOperation operation = _energyBasedCrafter.ProvideInputFuel(slot.Stack);
            Debug.Log("ProvideFuelTask " + operation.Result);
            switch(operation.Result)
            {
                case AddItemOperationResult.AddedAll:
                    taskRunState = TaskRunState.Finished;
                    break;
                case AddItemOperationResult.PartiallyAdded:
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
        public InventoryBase Inventory;
        [SerializeField]
        public ItemCategory Category;

        public override void GetComponentsReferences(GameObject target)
        {
            Debug.Log("set data " + target);
            Inventory = target.GetComponent<InventoryBase>();
        }
    }
}
