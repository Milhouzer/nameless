using System.Collections.Generic;
using UnityEngine;
using Milhouzer.InventorySystem.Restrictions;
using System.Linq;
using Milhouzer.InventorySystem.ItemProcessing;
using System.Text;

namespace Milhouzer.InventorySystem
{
    [System.Serializable]
    public class SupportProcessRestriction : AddItemRestriction
    {
        [SerializeField]
        List<ProcessType> SupportedProcess = new();

        public override bool IsSatisfied(IItemData item)
        {
            bool result = SupportedProcess.Intersect(item.SupportedProcesses).Count() > 0;
            StringBuilder stringBuilder = new StringBuilder();
            foreach(ProcessType process in SupportedProcess)
            {
                stringBuilder.AppendFormat($"{process}, ");
            }
            stringBuilder.Append("//////");
            foreach(ProcessType process in item.SupportedProcesses)
            {
                stringBuilder.AppendFormat($"{process}, ");
            }
            stringBuilder.AppendLine();
            stringBuilder.AppendFormat($"RESULT: {result}");

            Debug.Log(stringBuilder);
            return result;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(ProcessType process in SupportedProcess)
            {
                stringBuilder.AppendFormat($"{process}, ");
            }
            return stringBuilder.ToString();
        }
    }
}
