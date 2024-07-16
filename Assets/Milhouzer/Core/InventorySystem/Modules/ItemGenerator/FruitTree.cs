using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Milhouzer.Common.Interfaces;
using System;

namespace Milhouzer.Core.InventorySystem
{
    [Obsolete]
    public class FruitTree : PeriodicItemGenerator, IItemHolder, IInspectable
    {
        [SerializeField]
        private List<Transform> GenerationSpots = new();

        [SerializeField]
        private FruitTreeInfos Infos;

        private bool _holdSingleItem = false;
        public bool HoldSingleItem => _holdSingleItem;
        public int Capacity => GenerationSpots.Count;

        public List<IItemStack> _items = new();

        public List<IItemStack> Items => _items;

        public Transform WorldTransform => transform;

        private List<GameObject> _instanciatedItems = new();

        public event IItemHolder.PickUpEvent OnPickedUp;

        public override void Generate()
        {
            if(_items.Count < GenerationSpots.Count)
            {
                ItemStack stack = _table.GetRandomUnitaryStack();
                Hold(stack);
                _count++;
            }
        }

        public override Vector3 GetGenerationPosition()
        {
            return GenerationSpots[_items.Count].position;
        }

        public AddItemOperation Hold(IItemStack stack)
        {          
            GameObject itemGO = InventoryManager.RenderItemModel(stack.Item, GetGenerationPosition(), transform);
            
            _items.Add(stack);  
            _instanciatedItems.Add(itemGO);

            return new AddItemOperation(AddItemOperationResult.AddedAll, stack.Item, stack.Amount);
        }

        public AddItemOperation Hold(List<IItemStack> stacks)
        {
            foreach (IItemStack stack in stacks)
            {
                Hold(stack);
            }

            return new AddItemOperation(AddItemOperationResult.AddedAll, null, stacks[0].Amount);
        }

        public RemoveItemOperation Pickup(IInventory inventory)
        {
            int elected = ElectStackForPicking();
            if(elected == -1)
                return RemoveItemOperation.RemovedNone();

            AddItemOperation operation = inventory.AddItem(_items[elected]);
            
            Destroy(_instanciatedItems[elected]);

            _items.RemoveAt(elected);
            _instanciatedItems.RemoveAt(elected);

            OnPickedUp?.Invoke();

            return new RemoveItemOperation(RemoveItemOperationResult.RemovedAll, null, operation.Added);
        }

        private int ElectStackForPicking()
        {
            return _items.Count != 0 ? 0 : -1;
        }

        public Dictionary<string, object> SerializeUIData()
        {
            return new Dictionary<string, object>()
            {
                {"Panel","FruitTree"},
                {"FruitTreeInfos", Infos}
            };
        }
    }

        [System.Serializable]
        public struct FruitTreeInfos
        {
            public string Name;
            public string Production;
            public float Size;

        }
}
