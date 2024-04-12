namespace Milhouzer.InventorySystem.CraftingSystem
{
    using System.Collections;
    using System.Linq;
    using UnityEngine;
    
    public class Crafter : MonoBehaviour, ICrafter
    {
        [Header("Crafter section")]
        /* ====== DEBUG ====== */
        public string RecipeDebug;

        /* ====== COMPONENT ====== */
        
        [SerializeField]
        protected InventoryBase _inputIngredients;
        public InventoryBase InputIngredients => _inputIngredients;
        [SerializeField]
        protected InventoryBase _output;
        public InventoryBase Output => _output;

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

        public virtual RemoveItemOperation PickupOutput(InventoryBase inventory)
        {
            int left = _output.Slots.Count;
            for (int i = _output.Slots.Count - 1; i >= 0 ; i--)
            {
                IItemStack stack = _output.Slots[i].Stack;
                AddItemOperation operation = inventory.AddItem(stack);
                if(operation.Result == AddItemOperationResult.AddedAll)
                {
                    _output.RemoveSlot(i);
                    left--;
                }
            }
            return new RemoveItemOperation();
        }

        public virtual bool TryStartCraft()
        {
            if(InventoryManager.Instance.CanCraftItem(_inputIngredients, RecipeDebug, Process))
            {
                CurrentCraftProcess = StartCoroutine(CraftProcess());
                return true;
            }
            return false;
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