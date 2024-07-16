using System.Collections.Generic;

namespace Milhouzer.Core.InventorySystem.ItemProcessing
{
    public interface IProcessable
    {
        List<ProcessType> SupportedProcesses { get; }
    }
}