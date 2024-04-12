using UnityEngine;

namespace Milhouzer.InventorySystem
{
    public interface IObjectGenerator<T> 
    {
        bool CanGenerate();
        void Generate();
        int MaxCount { get; }
    }

    public interface IItemGenerator : IObjectGenerator<BaseItemData>
    {
        ItemDropTable Table { get; }

        public Vector3 GetGenerationPosition();
        public Quaternion GetGenerationRotation();
    }
}

