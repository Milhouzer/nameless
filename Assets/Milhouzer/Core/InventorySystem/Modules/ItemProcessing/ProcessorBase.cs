using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Milhouzer.Core.InventorySystem.ItemProcessing
{
    public abstract class ProcessorBase : MonoBehaviour, IProcessor
    {
        [SerializeField]
        protected ProcessType _process;
        public ProcessType Process => _process;

        protected Process _pendingProcess;
        public IProcess PendingProcess => _pendingProcess;

        public bool IsProcessing => _pendingProcess.IsDefault();

        public abstract bool StartProcess();
        public abstract void FinishProcess();

        public bool CanProcess(IProcessable processable)
        {
            return processable.SupportedProcesses.Contains(_process);
        }

        protected virtual void Update()
        {
            if(IsProcessing)
                return;

            Debug.Log("process: " + _pendingProcess);
            _pendingProcess.Update(Time.deltaTime);
            if(_pendingProcess.IsFinished())
            {
                FinishProcess();
            }
        }
    }
}

