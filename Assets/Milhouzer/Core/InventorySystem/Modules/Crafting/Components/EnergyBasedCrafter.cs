namespace Milhouzer.Core.InventorySystem.CraftingSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Milhouzer.Common.Interfaces;
    using Milhouzer.Common.Utility;
    using UnityEngine;
    
    public class EnergyBasedCrafter : Crafter, IEnergyBasedCrafter, IInspectable
    {

        [Header("EnergyBasedCrafter section")]
        /* ====== COMPONENT ====== */
        [SerializeField]
        private IInventory _inputFuel;
        public IInventory InputFuel => _inputFuel;

        private Timer powerTimer;

        public Transform WorldTransform => gameObject.transform;

        private void OnEnable() {
            
            powerTimer = new Timer(0, TryStartCraft);
        }

        private void Update() {
            Work(Time.deltaTime);
        }

        protected bool CanWork()
        {
            return true;
        }

        protected void Work(float elapsedTime)
        {
            if(IsCrafting)
            {
                if(!TryBurnCurrentFuel() && !BurnNewFuel())
                {
                    StopCraft();
                }
            }
            else
            {
                TryBurnCurrentFuel();
            }
        }

        private bool BurnNewFuel()
        {
            if(powerTimer.IsRunning)
                return false;

            IItemSlot slot = _inputFuel.Slots.First();
            if(slot == null)
                return false;

            IItemStack currentFuel = slot.Stack;

            FloatItemProperty power = currentFuel.Item.Data.GetProperty("FUEL_POWER") as FloatItemProperty;
            if(power == null)
                return false;

            powerTimer.AddDuration(power.GetValue());
            powerTimer.Start();

            return _inputFuel.RemoveItem(slot.Item).Result == RemoveItemOperationResult.RemovedAll;
        }

        private bool TryBurnCurrentFuel()
        {
            if(!powerTimer.IsRunning)
                return false;

            powerTimer.Update();
            return true;
        }

        public AddItemOperation ProvideInputFuel(IItemStack item)
        {
            AddItemOperation operation = _inputFuel.AddItem(item);
            Debug.Log("Prvide fuel" + operation.Result);
            if(operation.Result != AddItemOperationResult.AddedNone)
            {
                TryStartCraft();
            }

            return operation;
        }

        protected override IEnumerator CraftProcess()
        {
            Debug.Log("Process craft : " + (Progress + Time.deltaTime) + " with fuel power : " + powerTimer.Remaining);
            
            while(Progress <= 3f)
            {
                if(!powerTimer.IsRunning)
                {
                    StopCraft();
                    yield break;
                }
                Progress += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Craft();
            StopCraft();
        }

        protected override CraftOperationResult Craft()
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

        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Panel", "EnergyBasedCrafter"},
            };
        }
    }
}