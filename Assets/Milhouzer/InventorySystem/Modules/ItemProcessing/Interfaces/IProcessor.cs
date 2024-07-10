using System.Collections.ObjectModel;

namespace Milhouzer.InventorySystem.ItemProcessing
{
    public interface IProcessor
    {
        /// <summary>
        /// Process applied by the processor
        /// </summary>
        /// <value></value>
        ProcessType Process { get; }

        /// <summary>
        /// Current pending processes
        /// </summary>
        /// <value></value>
        /// <TODO>
        /// Handle multiple process at the same time ?
        /// </TODO>
        IProcess PendingProcess { get; }
        
        public bool IsProcessing { get; }

        /// <summary>
        /// Try process a processable entity
        /// </summary>
        /// <param name="processable"></param>
        /// <returns></returns>
        bool StartProcess();

        void FinishProcess();
    }
}