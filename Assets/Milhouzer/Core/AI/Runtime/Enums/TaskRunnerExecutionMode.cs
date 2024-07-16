namespace Milhouzer.Core.AI
{
    /// <summary>
    /// Execution mode of a task runner :
    /// - ExecuteOnce : discard a task when executed.
    /// - AutoExecute : loop over tasks
    /// - Planning : taskrunner is in planning mode.
    /// </summary>
    /// <TODO>
    /// Remove ?
    /// </TODO>
    public enum TaskRunnerExecutionMode
    {
        ExecuteOnce,
        AutoExecute,
        Planning,
        // ExecuteDefaultTask => Add default task that the runner should execute when it's empty.
    }
}