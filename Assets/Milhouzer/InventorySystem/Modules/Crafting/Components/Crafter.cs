namespace Milhouzer.InventorySystem.CraftingSystem
{
    using System;
    using System.Collections;
    using System.Linq;
    using UnityEngine;
    
    [Obsolete]
    public class Crafter : MonoBehaviour, ICrafter
    {
        [Header("Crafter section")]
        /* ====== DEBUG ====== */
        public string RecipeDebug;

        /* ====== COMPONENT ====== */
        
        [SerializeField]
        protected IInventory _inputIngredients;
        public IInventory InputIngredients => _inputIngredients;
        [SerializeField]
        protected IInventory _output;
        public IInventory Output => _output;

        [SerializeField]
        protected CraftingProcess _process;
        public CraftingProcess Process => _process;
        
        protected Coroutine CurrentCraftProcess;

        public bool IsCrafting { get; protected set; }
        public float Progress { get; protected set; }

        public virtual AddItemOperation ProvideInputIngredient(IItemStack item)
        {
            AddItemOperation operation = _inputIngredients.AddItem(item);
            if(operation.Result != AddItemOperationResult.AddedNone)
            {
                TryStartCraft();
            }

            return operation;
        }
        
        public virtual RemoveItemOperation PickupOutput(IInventory inventory)
        {
            RemoveItemOperation result = new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, null, 0);
            int left = _output.Slots.Count;
            for (int i = _output.Slots.Count - 1; i >= 0 ; i--)
            {
                IItemStack stack = _output.Slots[i].Stack;
                AddItemOperation operation = inventory.AddItem(stack);
                if(operation.Result != AddItemOperationResult.AddedAll)
                {
                    result.CombineOperation(_output.RemoveItem(stack.Item));
                    left--;
                }
            }
            return new RemoveItemOperation();
        }

        public virtual void TryStartCraft()
        {
            if(InventoryManager.Instance.CanCraftItem(_inputIngredients, RecipeDebug, Process))
            {
                CurrentCraftProcess = StartCoroutine(CraftProcess());
            }
        }
        
        public virtual void StopCraft()
        {
            StopCoroutine(CurrentCraftProcess);
            Progress = 0;
            IsCrafting = false;
        }

        protected virtual IEnumerator CraftProcess()
        {
            Debug.Log("Process craft : " + (Progress + Time.deltaTime));
            while(Progress <= 3f)
            {
                Progress += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Craft();
            StopCraft();
        }

        protected virtual CraftOperationResult Craft()
        {
            bool crafted = InventoryManager.Instance.CraftItem
            (
                _inputIngredients, 
                _output,
                RecipeDebug, 
                Process
            );

            Debug.Log("Crafted " + RecipeDebug + " " +  crafted);
            if(!crafted)
            {
                return CraftOperationResult.MissingIngredients;
            }

            return CraftOperationResult.Success;
        }        
    }
}