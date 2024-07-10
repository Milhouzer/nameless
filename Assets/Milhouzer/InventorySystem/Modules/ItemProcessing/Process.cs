using System;

namespace Milhouzer.InventorySystem.ItemProcessing
{
    /// <summary>
    /// Process wrapper.
    /// Tracks the state of a process
    /// </summary>
    public struct Process : IProcess
    {
        public ProcessType ProcessType { get; private set; }
        public readonly bool IsFinished() { return _elapsedTime > _duration; }

        public Action OnFinish { get; private set; }
        
        private float _elapsedTime;
        private float _duration;
    
        public Process(ProcessType processType, float duration, Action onFinish)
        {
            ProcessType = processType;
            OnFinish = onFinish;
            _duration = duration;
            _elapsedTime = 0;
        }

        public void Update(float dt)
        {
            _elapsedTime += dt;
        }
        
        // Define a method to check if the instance is the default value
        public readonly bool IsDefault()
        {
            return Equals(default(Process));
        }
    }

    public interface IProcess {
        public ProcessType ProcessType { get; }
        public bool IsFinished();
        public void Update(float dt);
    }
}
