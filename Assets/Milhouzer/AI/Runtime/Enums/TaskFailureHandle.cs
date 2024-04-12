
namespace Milhouzer.AI
{
    /// <summary>
    /// Describe how the failure of the task is handled.
    /// </summary>
    public enum TaskFailureHandle
    {
        Pass,
        Retry,
        BreakSequence
    }
}