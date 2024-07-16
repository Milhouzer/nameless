using UnityEngine;
using Milhouzer.Core.InventorySystem;
using Milhouzer.Core.InventorySystem.CraftingSystem;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI.Modules.InventorySystem
{
    [System.Serializable]
    public class ProvideIngredients : TaskBase
    {
        [SerializeField]
        protected new ProvideIngredientsTaskData _data;
        public new ProvideIngredientsTaskData Data => _data;

        protected ICrafter _crafter;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {            
            _data = data as ProvideIngredientsTaskData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(ProvideIngredients)}: Invalid data type. Expected {nameof(ProvideIngredientsTaskData)}, but received {data?.GetType().Name ?? "null"}.");

             _crafter = target.GetComponent<ICrafter>();
        
        }
        public override void Start()
        {
            if(_data.Inventory != null && _crafter != null)
            {
                IItemSlot slot = _data.Inventory.FindItemSlot(x => x.Stack.Item.Data.Category == _data.Category);
                if(slot == null && slot.Item.Data != null)
                {
                    Debug.Log("no fueld found on " + _data.Inventory);
                    taskRunState = TaskRunState.Failed;
                    return;
                }

                AddItemOperation operation = _crafter.ProvideInputIngredient(slot.Stack);
                
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
            }
            else
            {
                Debug.Log("Inventory : " + _data.Inventory + ", cookStation : " + _crafter+ "/ " + _runner + "/ " + _target);
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
    public class ProvideIngredientsTaskData : BaseTaskData
    {

        [SerializeField]
        private string _name = "ProvideIngredients Task Data";
        public new string Name => _name;
        [SerializeField]
        public ItemCategory Category;
        
        [HideInInspector]
        public IInventory Inventory;

        public override void GetComponentsReferences(GameObject target, GameObject instigator)
        {
            Debug.Log("set data " + target);
            Inventory = target.GetComponent<IInventory>();
        }
    }
}
