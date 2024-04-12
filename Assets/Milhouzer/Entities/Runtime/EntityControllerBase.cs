using System.Collections.Generic;
using System.Collections.ObjectModel;
using Milhouzer.AI;
using UnityEngine;

namespace Milhouzer.Entities
{
    public class EntityControllerBase : MonoBehaviour, IEntityController
    {
        [SerializeField]
        bool _autoStart = true;
        public bool AutoStart => _autoStart;

        [SerializeField]
        protected List<ITask> _tasks = new();
        public ReadOnlyCollection<ITask> Tasks => _tasks.AsReadOnly();

        [SerializeField]
        protected int _current = 0;
        public int Current => _current;

        protected bool _isRunning;
        public bool IsRunning => _isRunning;

        public GameObject Owner => gameObject;

        [SerializeField]
        protected TaskRunnerExecutionMode _executionMode = TaskRunnerExecutionMode.ExecuteOnce;

        public event AddTask_EventHandler TaskAdd;

        public TaskRunnerExecutionMode ExecutionMode 
        {
            get { return _executionMode; }
            set { _executionMode = value; }
        }

        protected virtual void Awake()
        {
            if(_autoStart)
            {
                StartRunner(true);
            }
        }

        public void AddTask(ITask task)
        {
            bool Cancel = false;
            TaskAdd?.Invoke(task, ref Cancel);

            if(Cancel)
                return;
            
            Reset();
            _tasks.Add(task);
            if(!_isRunning)
                StartRunner(true);
        }

        public void AddTasks(List<ITask> tasks)
        {
            Reset();

            foreach(ITask task in tasks)
            {
                _tasks.Add(task);
            }

            if(!_isRunning)
                StartRunner(true);
        }

        public void StartRunner(bool reset = false)
        {
            if(_tasks.Count == 0)
            {
                Stop();
                return;
            }

            _isRunning = true;
            if(reset)
                _current = 0;
            
            _tasks[_current].Start();
        }

        public void Run()
        {
            switch(_tasks[_current].State)
            {
                case TaskRunState.Finished:
                    EndTask();
                    break;
                case TaskRunState.Failed:
                    EndTask();
                    break;
                case TaskRunState.Waiting:
                    _tasks[_current].Start();
                    break;
                case TaskRunState.Running:
                    _tasks[_current].Execute();
                    break;
            }
        }

        public void EndTask()
        {
            _tasks[_current].Stop();
            if(_executionMode == TaskRunnerExecutionMode.ExecuteOnce)
            {
                _tasks.RemoveAt(_current);
                _current--;
            }
            Next();
        }

        public void Next()
        {
            switch(_executionMode)
            {
                case TaskRunnerExecutionMode.ExecuteOnce:
                    _current++;
                    if(_current >= _tasks.Count)
                    {
                        Stop();
                        return;
                    }
                    break;
                case TaskRunnerExecutionMode.AutoExecute:
                    _current = (_current + 1 == _tasks.Count) ? 0 : _current + 1;
                    break;
            }

            _tasks[_current].Start();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Reset()
        {
            Stop();
            _current = 0;
            _tasks = new();
        }

        protected void Update() 
        {
            if(_isRunning)
            {
                Run();
            }
        }
    }
}
