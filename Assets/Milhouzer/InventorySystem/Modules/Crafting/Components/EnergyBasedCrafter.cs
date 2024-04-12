namespace Milhouzer.InventorySystem.CraftingSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Milhouzer.Common.Interfaces;
    using UnityEngine;
    
    public class EnergyBasedCrafter : Crafter, IEnergyBasedCrafter, IInspectable
    {

        [Header("EnergyBasedCrafter section")]
        /* ====== COMPONENT ====== */
        [SerializeField]
        private InventoryBase _inputFuel;
        public InventoryBase InputFuel => _inputFuel;

        public float RemainingPower { get; private set; }

        public Transform WorldTransform => gameObject.transform;

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
                if(!TryBurnCurrentFuel(elapsedTime) && !BurnNewFuel(elapsedTime))
                {
                    StopCraft();
                }
            }
            else
            {
                TryBurnCurrentFuel(elapsedTime);
            }
        }

        private bool BurnNewFuel(float elapsedTime)
        {
            if(RemainingPower >= 0)
                return false;

            ItemSlot slot = _inputFuel.Slots.First();
            if(slot == null)
                return false;

            IItemStack currentFuel = slot.Stack;

            ItemProperty power = currentFuel.Item.Data.GetProperty("fuel_power");
            RemainingPower = power.FloatValue - elapsedTime + RemainingPower;

            return _inputFuel.RemoveItem(slot.Stack.Item).Result == RemoveItemOperationResult.RemovedAll;
        }

        private bool TryBurnCurrentFuel(float elapsedTime)
        {
            if(RemainingPower <= 0)
                return false;

            RemainingPower -= elapsedTime;
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
            Debug.Log("Process craft : " + (Progress + Time.deltaTime) + " with fuel power : " + RemainingPower);
            while(Progress <= 3f)
            {
                if(RemainingPower <= 0)
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


        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Type", "EnergyBasedCrafter"},
            };
        }
    }
}