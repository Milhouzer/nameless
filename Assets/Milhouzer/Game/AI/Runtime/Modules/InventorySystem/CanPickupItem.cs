using UnityEngine;
using Milhouzer.Core.InventorySystem;
using Milhouzer.Core.InventorySystem.Restrictions;
using Milhouzer.Core.AI;

namespace Milhouzer.Game.AI.Modules.InventorySystem
{
    /// <remarks>
    /// Task is not really relevant for the moment as InventoryBase (which is the only inventory of the game) always check for restrictions before doing any
    /// add operation.
    /// </remarks>
    [System.Serializable]
    public class CanPickupItem : TaskBase
    {
        [SerializeField]
        protected new CanPickupItemData _data;
        public new CanPickupItemData Data => _data;

        protected ItemStack _itemStack;

        protected override void OnInitialize(ITaskRunner runner, GameObject target, ITaskData data)
        {
            _data = data as CanPickupItemData;
            if(_data == null)
                throw new System.InvalidOperationException($"Initialization failed for {nameof(CanPickupItem)}: Invalid data type. Expected {nameof(CanPickupItemData)}, but received {data?.GetType().Name ?? "null"}.");

             _itemStack = new ItemStack(_data.Item.Data, _data.Item.Amount);
        }

        public override void Start()
        {
            if(_data.Restrictions != null)
            {
                if(_data.Restrictions.SatisfyRestrictions(_data.Item.Data))
                {
                        taskRunState = TaskRunState.Finished;
                }
                taskRunState = TaskRunState.Failed;
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
    public class CanPickupItemData : BaseTaskData
    {

        [SerializeField]
        private string _name = "CanPickupItem Task Data";
        public new string Name => _name;

        private InventoryRestrictions _restrictions;
        public InventoryRestrictions Restrictions => _restrictions;

        public ItemStackDefinition Item;

        public override void GetComponentsReferences(GameObject target, GameObject Instigator)
        {
            _restrictions = target.GetComponent<InventoryRestrictions>();
        }
    }
}
