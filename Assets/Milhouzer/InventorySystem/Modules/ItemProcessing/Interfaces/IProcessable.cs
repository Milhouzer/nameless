using System.Collections.Generic;

namespace Milhouzer.InventorySystem.ItemProcessing
{
    public interface IProcessable
    {
        List<ProcessType> SupportedProcesses { get; }
    }
}