namespace Milhouzer.InventorySystem
{
    public struct RemoveItemOperation 
    {

        public RemoveItemOperationResult Result {get; private set;}
        public int Removed {get; private set;}

        public RemoveItemOperation(RemoveItemOperationResult result, int removed)
        {
            Result = result;
            Removed = removed;
        }

        public static RemoveItemOperation RemovedNone() => new RemoveItemOperation(RemoveItemOperationResult.RemovedNone, 0);        
    }
}
