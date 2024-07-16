using UnityEngine;
using System.Collections.Generic;

namespace Milhouzer.Core.InventorySystem
{
    [CreateAssetMenu(fileName = "SegmentedInventoryDataInjector", menuName = "SegmentedInventoryDataInjector", order = 0)]
    public class SegmentedInventoryDataInjector : InventoryDataInjector {
        

            [SerializeField]
            public List<InventorySegmentation> segmentations = new();
            private void OnValidate() {
                int tot = 0;
                foreach(InventorySegmentation segmentation in segmentations){
                    tot+=segmentation.Length;
                }
            }
    }
}